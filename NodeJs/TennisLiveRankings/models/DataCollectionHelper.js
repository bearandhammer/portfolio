import { elementSelectors, httpUtils } from '../utility/base';

// Axios (AJAX for returning live ranking HTML 'raw' data) and Cheerio included to scrape HTML and provide manipulated content
const axios = require('axios');
const cheerio = require('cheerio');
const opn = require('opn');

export default class DataCollectionHelper {
    constructor(url, proxy = httpUtils.proxy) {
        this.url = url;
        this.proxy = proxy;
    }

    // Return live ranking results (from the designated URL - from an HTML page currently)
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

    async getSpecificPlayerResults() {
        try {
            const res = await axios.get(`${this.proxy}https://www.google.com/search?safe=off&q=atp+tour+john+isner`, { 
                crossdomain: true, 
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                }
            });

            this.specificPlayerResults = res.data;
        } catch (error) {
            console.log(error);
        }
    }

    getPlayerLink() {
        if (this.specificPlayerResults) {
            const $ = cheerio.load(this.specificPlayerResults);
            const urlHref = $("a[href*='https://www.atptour.com/en/players/'][href*='/overview']").attr('href');
            
            if (urlHref) {
                const physicalUrl = urlHref.substring(urlHref.indexOf('https'), urlHref.indexOf('&sa='));

                if (physicalUrl) {
                    opn(physicalUrl);
                }   
            }

            console.log(urlHref);
        }
    }

    getPlayerDataFromHtml() {
        // Temp code only - attempt to scrape the HTML using fairly fast/loose index based grabs on td text
        const playerDetailsTemp = [];

        if (this.liveRankingResultsHtml) {
            // Use cheeriojs to obtain the 'ranking' HTML table (just going for the first 10 results, for now) - just want to inspect the text, as a test
            const $ = cheerio.load(this.liveRankingResultsHtml);
            const elements = $(elementSelectors.atpTableId).children('tbody').find('tr');

            let country;
            elements.slice(0, 20).each((i, elem) => {
                if (i % 2 === 0) {
                    country = $(elem).find('td').eq(5).text();

                    playerDetailsTemp.push({
                        rank: parseInt($(elem).find('td').eq(0).text().trim()),
                        name: $(elem).find('td').eq(3).text(),
                        age: Math.floor(parseFloat($(elem).find('td').eq(4).text())),
                        country: country.substring(0, country.length - 1),
                        points: parseInt($(elem).find('td').eq(6).text()),
                    });
                }
            });   
        }

        return playerDetailsTemp;
    }
}