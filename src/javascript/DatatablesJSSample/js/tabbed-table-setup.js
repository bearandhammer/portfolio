$(function () {
    // Configure the dataTablesNetTabbedModule (handling stock table setup, options & events). 
    // Then, register tables (using an each here so that for automatic table detection - more robust if Ids change also) - for this tabbed setup we have a single search control
    dataTablesNetTabbedModule.configure(['#SearchEntitiesInput']);
    $('table').each(function () {
        dataTablesNetTabbedModule.registerDataTableById('#' + $(this).attr('id').trim());
    });
});