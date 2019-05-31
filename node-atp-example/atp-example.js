// Excellent tutorial for using import/export (ES6 gubbins) here! https://timonweb.com/tutorials/how-to-enable-ecmascript-6-imports-in-nodejs/
import AtpDataCollectionHelper from "./Models/AtpDataCollectionHelper";
import { httpUtils } from "./Utility/base";

// Use 'express' for our stock server (handles routing, etc.)
const express = require('express');

// Some very simple debug switching, if needed along with our server variable
const debug = true;
const app = express();

app.get('/', async (req, res) => {
    if (debug) {
        console.log('Request logged for a resource...');
    }

    // Quick helper class to manage the return of results/manipulation of data
    const dataCollectionHelper = new AtpDataCollectionHelper(httpUtils.atpLiveRankingSite);
    await dataCollectionHelper.getResults();  

    if (debug) {
        // Confirm that the data type is of type 'string' (dummy)
        console.log(`Results data type: ${typeof dataCollectionHelper.liveRankingResultsHtml}`);
    }

    const playerDetailsTemp = dataCollectionHelper.getPlayerJsonFromHtml();

    // Again, return result as JSON, for now (until we do some proper templating/routing, etc.)
    res.setHeader('Content-Type', 'application/json')
    res.send(JSON.stringify(playerDetailsTemp));
});

// Kick off our server!
app.listen(httpUtils.port, '127.0.0.1', () => console.log(`Server started. Listening on 127.0.0.1 on port ${httpUtils.port}...`));