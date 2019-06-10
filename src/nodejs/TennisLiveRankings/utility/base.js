export const elementSelectors = {
    atpTableId: '#u868'
};

export const httpUtils = {
    atpLiveRankingSite: 'https://live-tennis.eu/en/atp-live-ranking',
    wtaLiveRankingSite: 'https://live-tennis.eu/en/wta-live-ranking',
    googlePlayerSearch: 'https://www.google.com/search?safe=off&q=atp+tour+',
    proxy: 'https://cors-anywhere.herokuapp.com/',
    host: '127.0.0.1',
    port: 1337
}

export const cacheUtils = {
    stockTtl: 300,
    stockCheckPeriod: 330,
    keys: {
        playerData: 'playerData',
        profileData: 'profileData_'
    }
}

export const routes = {
    root: '/',
    overview: '/overview',
    profile: '/profile'
}

export const views = {
    overview: 'overview',
    profile: 'profile'
}