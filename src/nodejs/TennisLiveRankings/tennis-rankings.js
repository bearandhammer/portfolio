// Excellent tutorial for using import/export (ES6 gubbins) here! https://timonweb.com/tutorials/how-to-enable-ecmascript-6-imports-in-nodejs/
import DataCollectionHelper from "./models/DataCollectionHelper";
import DataCleanser from './models/DataCleanser';
import { httpUtils, cacheUtils, routes, views } from "./utility/base";

// Use 'express' for our stock server (handles routing, etc.), along with some extra utilities for serving favicons and general path generation
const express = require('express');
const favicon = require('serve-favicon');
const path = require('path');
const NodeCache = require( "node-cache" );

// Declare the variable that underpins our Node.js server
const app = express();
const appCache = new NodeCache({ 
    stdTTL: cacheUtils.stockTtl, 
    checkperiod: cacheUtils.stockCheckPeriod 
});

// General utility functions acting as a gateway to our DataCollectionHelper utility and underlying cache mechanisms
async function getPartialPlayerData() {
    let playerData = appCache.get(cacheUtils.keys.playerData);

    if (!playerData) {
        // Create instances of helper types to manage the return of results/manipulation of data
        const atpDataCollectionHelper = new DataCollectionHelper(httpUtils.atpLiveRankingSite),
            wtaDataCollectionHelper = new DataCollectionHelper(httpUtils.wtaLiveRankingSite);

        // Generate two promises here and then await them separately (to get a performance buff)
        const atpPromise = atpDataCollectionHelper.getResults(),
            wtaPromise = wtaDataCollectionHelper.getResults();

        await atpPromise;
        await wtaPromise;

        // Setup player data (ATP/WTA) and cache the results (as this is fairly expensive to obtain)
        playerData = {
            atpPlayerData: atpDataCollectionHelper.getPlayerDataFromHtml(),
            wtaPlayerData: wtaDataCollectionHelper.getPlayerDataFromHtml()
        }

        appCache.set(cacheUtils.keys.playerData, playerData);   
    }

    return playerData;
}

async function getFullProfileData(req) {
    const cleanser = new DataCleanser();
    const name = cleanser.cleanPlayerName(req.query.name);
    const cacheKey = `${cacheUtils.keys.profileData}${ name }`;

    // Player WTA/ATP profiles are obtained via scraping a Google search results page, so obtain this separately 
    let playerLink = appCache.get(cacheKey);;

    if (!playerLink) {
        const atpDataCollectionHelper = new DataCollectionHelper(httpUtils.googlePlayerSearch);
        await atpDataCollectionHelper.getSpecificPlayerResults(name);

        playerLink = atpDataCollectionHelper.getPlayerLink(req.query.type);

        // Cache the link obtained, again as their is a hit in retrieving this each time
        appCache.set(cacheKey, playerLink);  
    }

    // Performance rethink needed here (although caching helps) - obtain the partial player data again and append the link (gives us full player data!)
    const allPlayers = await getPartialPlayerData();
    
    const discoveredPlayer = req.query.type === 'atp' 
        ? allPlayers.atpPlayerData.find(player => cleanser.cleanPlayerName(player.name) === name)
        : req.query.type === 'wta' 
        ? allPlayers.wtaPlayerData.find(player => cleanser.cleanPlayerName(player.name) === name)
        : null;

    // TBC - error handling
    if (discoveredPlayer) {
        discoveredPlayer.playerLink = playerLink;
    }  
    
    return discoveredPlayer;
}

// Server setup - configured the EJS view engine, static resources (from the 'public' folder) as well as the defined routes
app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));
app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.get([routes.root, routes.overview], async (req, res) => {    
    res.render(views.overview, { 
        playerData: await getPartialPlayerData()
    });
})
.get(routes.profile, async (req, res) => {
    res.render(views.profile, { 
        fullPlayerData: await getFullProfileData(req)
    });
});
// Kick off our server!
app.listen(httpUtils.port, httpUtils.host, () => console.log(`Server started. Listening on ${ httpUtils.host } on port ${httpUtils.port}...`));