// Excellent tutorial for using import/export (ES6 gubbins) here! https://timonweb.com/tutorials/how-to-enable-ecmascript-6-imports-in-nodejs/
import DataCollectionHelper from "./models/DataCollectionHelper";
import { httpUtils } from "./utility/base";

// Use 'express' for our stock server (handles routing, etc.), along with some extra utilities for serving favicons and general path generation
const express = require('express');
const favicon = require('serve-favicon');
const path = require('path');

// Some very simple debug switching, if needed along with our server variable
const debug = true;
const app = express();

// Server setup - configured the EJS view engine, static resources (from the 'public' folder) as well as the defined routes
app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));
app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.get('/', async (req, res) => {
    if (debug) {
        console.log('Request logged for a resource...');
    }

    // Quick helper class to manage the return of results/manipulation of data
    const atpDataCollectionHelper = new DataCollectionHelper(httpUtils.atpLiveRankingSite),
        wtaDataCollectionHelper = new DataCollectionHelper(httpUtils.wtaLiveRankingSite);

    const atpPromise = atpDataCollectionHelper.getResults(),
        wtaPromise = wtaDataCollectionHelper.getResults();

    await atpPromise;
    await wtaPromise;

    if (debug) {
        // Confirm that the data types are of type 'string' (dummy)
        console.log(`Results data type: ${typeof atpDataCollectionHelper.liveRankingResultsHtml}`);
        console.log(`Results data type: ${typeof wtaDataCollectionHelper.liveRankingResultsHtml}`);
    }

    const playerData = {
        atpPlayerData: atpDataCollectionHelper.getPlayerDataFromHtml(),
        wtaPlayerData: wtaDataCollectionHelper.getPlayerDataFromHtml()
    }

    // EJS test run
    res.render('overview', { 
        playerData: playerData
    });
});
// Kick off our server!
app.listen(httpUtils.port, '127.0.0.1', () => console.log(`Server started. Listening on 127.0.0.1 on port ${httpUtils.port}...`));