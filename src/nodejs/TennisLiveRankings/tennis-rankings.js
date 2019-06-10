// Excellent tutorial for using import/export (ES6 gubbins) here! https://timonweb.com/tutorials/how-to-enable-ecmascript-6-imports-in-nodejs/
import DataCollectionHelper from "./models/DataCollectionHelper";
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
async function getPlayerDataFromCache() {
    let playerData = appCache.get(cacheUtils.keys.playerData);

    if (!playerData) {
        // Quick helper class to manage the return of results/manipulation of data
        const atpDataCollectionHelper = new DataCollectionHelper(httpUtils.atpLiveRankingSite),
            wtaDataCollectionHelper = new DataCollectionHelper(httpUtils.wtaLiveRankingSite);

        const atpPromise = atpDataCollectionHelper.getResults(),
            wtaPromise = wtaDataCollectionHelper.getResults();

        await atpPromise;
        await wtaPromise;

        playerData = {
            atpPlayerData: atpDataCollectionHelper.getPlayerDataFromHtml(),
            wtaPlayerData: wtaDataCollectionHelper.getPlayerDataFromHtml()
        }

        appCache.set(cacheUtils.keys.playerData, playerData);   
    }

    return playerData;
}

async function getProfileDataFromCache(req) {
    const name = req.query.name
        .toLowerCase()
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, "")
        .replace(' ', '+');

    const cacheKey = `${cacheUtils.keys.profileData}${ name }`;

    let playerLink = appCache.get(cacheKey);;

    if (!playerLink) {
        const atpDataCollectionHelper = new DataCollectionHelper(httpUtils.googlePlayerSearch);
        await atpDataCollectionHelper.getSpecificPlayerResults(name);

        playerLink = atpDataCollectionHelper.getPlayerLink();

        appCache.set(cacheKey, playerLink);  
    }
    
    return playerLink;
}

// Server setup - configured the EJS view engine, static resources (from the 'public' folder) as well as the defined routes
app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));
app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.get([routes.root, routes.overview], async (req, res) => {    
    res.render(views.overview, { 
        playerData: await getPlayerDataFromCache()
    });
})
.get(routes.profile, async (req, res) => {
    res.render(views.profile, { 
        playerLink: await getProfileDataFromCache(req)
    });
});
// Kick off our server!
app.listen(httpUtils.port, httpUtils.host, () => console.log(`Server started. Listening on ${ httpUtils.host } on port ${httpUtils.port}...`));