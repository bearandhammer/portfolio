// Excellent tutorial for using import/export (ES6 gubbins) here! https://timonweb.com/tutorials/how-to-enable-ecmascript-6-imports-in-nodejs/
import DataCollectionHelper from "./models/DataCollectionHelper";
import { httpUtils } from "./utility/base";

// Use 'express' for our stock server (handles routing, etc.), along with some extra utilities for serving favicons and general path generation
const express = require('express');
const favicon = require('serve-favicon');
const path = require('path');
const NodeCache = require( "node-cache" );

// Declare the variable that underpins our Node.js server
const app = express();
const appCache = new NodeCache({ 
    stdTTL: 120, checkperiod: 140 
});

// General utility functions acting as a gateway to our DataCollectionHelper utility and underlying cache mechanisms
async function getPlayerDataFromCache() {
    let playerData = appCache.get('playerData');

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

        console.log('Caching player data data.');
        appCache.set('playerData', playerData);   
    }
    else {
        console.log('Using cached player data.');
    }


    return playerData;
}

async function getProfileDataFromCache(req) {
    // Well this isn't quite as intended - need to use the player name as the key for this to work!!!
    // let playerLink = appCache.get('playerProfile');;

    // if (!playerLink) {
    const name = req.query.name
        .toLowerCase()
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, "")
        .replace(' ', '+');

    const atpDataCollectionHelper = new DataCollectionHelper(httpUtils.googlePlayerSearch);
    await atpDataCollectionHelper.getSpecificPlayerResults(name);

    const playerLink = atpDataCollectionHelper.getPlayerLink();

    //     console.log('Caching player profile data.');
    //     appCache.set('playerProfile', playerLink);  
    // }
    // else {
    //     console.log('Using cached profile data.');
    // }

    return playerLink;
}

// Server setup - configured the EJS view engine, static resources (from the 'public' folder) as well as the defined routes
app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));
app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.get(['/', '/overview'], async (req, res) => {    
    res.render('overview', { 
        playerData: await getPlayerDataFromCache()
    });
})
.get('/profile', async (req, res) => {
    res.render('profile', { 
        playerLink: await getProfileDataFromCache(req)
    });
});
// Kick off our server!
app.listen(httpUtils.port, '127.0.0.1', () => console.log(`Server started. Listening on 127.0.0.1 on port ${httpUtils.port}...`));