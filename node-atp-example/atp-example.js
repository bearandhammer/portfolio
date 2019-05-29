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

            this.liveRankingResultsHtml = res;
            console.log('Got results.');
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

    // Quick test of cheerio
    // console.log('Using results.');
    // console.log(dataCollectionHelper.liveRankingResultsHtml);
    // const $ = cheerio.load('<div>test</div>');
    // console.log($.text());
    console.log('Using results.')
    console.log(dataCollectionHelper.liveRankingResultsHtml);
});
server.listen(1337, '127.0.0.1', () => {
    console.log('Server started. Listening on 127.0.0.1 on port 1337...');
});