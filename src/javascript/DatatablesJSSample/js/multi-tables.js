$(function () {
    // Configure the dataTablesNetTabbedModule (handling stock table setup, options & events). 
    // Then, register tables (using an each here so that for automatic table detection - more robust if Ids change also)
    dataTablesNetTabbedModule.configure(['#SearchFootballFixturesInput', '#SearchFoodFestivalsInput']);
    $('table').each(function () {
        dataTablesNetTabbedModule.registerDataTableById('#' + $(this).attr('id').trim());
    });
});