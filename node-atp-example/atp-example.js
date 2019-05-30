// Stock package for creating a simple node server
const http = require('http');

// Another utility package is used for url inspection/routing capabilities
const url = require('url');

// Axios (AJAX for returning live ranking HTML 'raw' data)
const axios = require('axios');

// Cheerio included to scrape HTML and provide manipulated content
const cheerio = require('cheerio')

class AtpDataCollectionHelper {
    constructor(url) {
        this.url = url;
        this.proxy = 'https://cors-anywhere.herokuapp.com/';
    }

    // Return ATP live ranking results (from the designated URL - from an HTML page currently)
    async getResults() {
        try {
            const res = await axios.get(`${this.proxy}${this.url}`, { 
                crossdomain: true, 
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                }
            });

            this.liveRankingResultsHtml = res.data;
        } catch (error) {
            console.log(error);
        }
    }
}

// Server setup - when our server is hit the callback function is invoked. Setup to listen to port 1337 on localhost
const server = http.createServer(async (req, res) => {
    console.log('Request logged!');

    // Quick helper class to manage the return of results/manipulation of data
    const dataCollectionHelper = new AtpDataCollectionHelper('https://live-tennis.eu/en/atp-live-ranking');
    await dataCollectionHelper.getResults();

    // Confirm that the data type is of type 'string' (dummy)
    console.log(`Results data type: ${typeof dataCollectionHelper.liveRankingResultsHtml}`);

    // Use cheeriojs to obtain the 'ranking' HTML table (just going for the first 10 results, for now) - just want to inspect the text, as a test
    const $ = cheerio.load(dataCollectionHelper.liveRankingResultsHtml);
    const elements = $('#u868').children('tbody').find('tr');

    // Temp code only - attempt to scrape the HTML using fairly fast/loose index based grabs on td text
    const playerDetailsTemp = [];

    elements.slice(0, 20).each((i, elem) => {
        if (i % 2 === 0) {
            playerDetailsTemp.push({
                rank: parseInt($(elem).find('td').eq(0).text().trim()),
                name: $(elem).find('td').eq(3).text(),
                age: Math.floor(parseFloat($(elem).find('td').eq(4).text())),
                country: $(elem).find('td').eq(5).text(),
                points: parseInt($(elem).find('td').eq(6).text()),
            });
        }
    });

    // Again, return result as JSON, for now (until we do some proper templating/routing, etc.)
    res.setHeader('Content-Type', 'application/json')
    res.end(JSON.stringify(playerDetailsTemp));
});
server.listen(1337, '127.0.0.1', () => {
    console.log('Server started. Listening on 127.0.0.1 on port 1337...');
});