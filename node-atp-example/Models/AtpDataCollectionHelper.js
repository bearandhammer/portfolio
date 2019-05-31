import { elementSelectors, httpUtils } from '../Utility/base';

// Axios (AJAX for returning live ranking HTML 'raw' data)  and Cheerio included to scrape HTML and provide manipulated content
const axios = require('axios');
const cheerio = require('cheerio');

export default class AtpDataCollectionHelper {
    constructor(url, proxy = httpUtils.proxy) {
        this.url = url;
        this.proxy = proxy;
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

    getPlayerJsonFromHtml() {
        // Temp code only - attempt to scrape the HTML using fairly fast/loose index based grabs on td text
        const playerDetailsTemp = [];

        // Use cheeriojs to obtain the 'ranking' HTML table (just going for the first 10 results, for now) - just want to inspect the text, as a test
        const $ = cheerio.load(this.liveRankingResultsHtml);
        const elements = $(elementSelectors.atpTableId).children('tbody').find('tr');

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

        return playerDetailsTemp;
    }
}