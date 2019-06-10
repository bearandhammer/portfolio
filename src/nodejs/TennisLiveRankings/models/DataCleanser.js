// Utility class that provides any methods for data cleansing
export default class DataCleanser {
    cleanPlayerName(name) {
        let cleanedName = name;

        // Handle providing a uniformly-cased name with accents removed
        if (cleanedName) {
            cleanedName = 
                cleanedName.toLowerCase()
                .normalize('NFD')
                .replace(/[\u0300-\u036f]/g, "");
        }

        return cleanedName;
    }
}