// Utility module for handling stock configuration/setups for datatables
var dataTablesNetTabbedModule = (function () {
    // Table discovery occurs (for searching) in a slightly different manner depending on the registered search input count
    let registeredSearchInputCount = 0;

    // Fields (stock table options and an array to store registered tables)
    var stockTableOptions,
        currentTables = [];

    // Private Functions

    // Utility function that sets up stock table options (if overrides are provided they are left alone). A new object is created, if the one passed in undefined
    function addTableOptionStockDefaults(sharedTableOptions) {
        sharedTableOptions = (typeof sharedTableOptions === 'undefined') ? {} : sharedTableOptions;

        return $.extend(sharedTableOptions, {
            "paging": false,
            "info": false,
            // Zero record message is difficult to alter/style so configuring directly is the easiest option
            "language": {
                "zeroRecords": '<span style="display:block; text-align:center; padding: 0.8rem 0 1.2rem 0;">No matching records found</span>'
            }
        });
    }

    // Utility function (which can be added) that hides unrequired elements after a datatable is generated (using the tableElement.DataTable() call)
    function hideDataTablesGeneratedElements() {
        $('.dataTables_filter').hide();
    }

    // Sets up stock event listeners (fixed currently to the known template structure - we can make it more flexible if required) - triggers 
    // for all tables registered within this module
    function setupEventListeners(searchInputIds) {
        if (searchInputIds && searchInputIds.length > 0) {
            // An unexpected use case, currently, is to have multiple search inputs with a tabbed interface. This will require (likely) further work to sure up
            let tabsPresent = $('.nav-tabs').length > 0;
            if (searchInputIds.length > 1 && tabsPresent) {
                throw 'A tabbed setup with multiple search inputs represents an untested configuration. Please inspect the code, implement and test thoroughly';
            }

            searchInputIds.forEach(searchInputId =>
            {
                $(document).on('shown.bs.tab', 'a[data-toggle="tab"]', function (e) {
                    searchActiveTable($(searchInputId));
                })

                if ($(searchInputId).length > 0) {
                    // Setup events only if the searchInputId is configured/discovered
                    $('.container-fluid').on('keyup', searchInputId, function () {
                        searchActiveTable($(this))
                    });

                    // Only perform the event hook if '.nav-tabs' exist. If a more complicated page configuration is needed we will only want events
                    // to hook up/fire (trigger) when the tab is tied to a table. Overly complex to cater for that at this time, however
                    if (tabsPresent) {
                        $('.container-fluid').on('click', '.nav-tabs', function () {
                            searchActiveTable($(searchInputId));
                        });
                    }

                    // Handle UI alterations on header click (up/down fractal arrows, based on asc/desc ordering)
                    $('.container-fluid').on('click', 'th', function () {
                        handleTableHeaderUI($(this));
                    });

                    registeredSearchInputCount++;
                }
            });
        }
    }

    // Returns the default column order table header, from the target table, if available
    function getDefaultOrderingColumnFromTable(discoveredTable) {
        var defaultOrderingColumn = discoveredTable.find("[class^='default-order-column-']");

        // Return the first 'th' if no specific default has been applied
        return defaultOrderingColumn.length === 0 ? discoveredTable.find('th').first() : defaultOrderingColumn;
    }

    // Private function for configuring the specific configurations on a table, based on stock options, on registration
    function setupTableSpecificOptions(discoveredTable, defaultOrderingColumn) {
        // Prepare a local, overridable copy of table options (for modification, if needed, but still based on stock settings)
        var specificTableOptions = $.extend({}, {}, stockTableOptions);

        // 1) Define those columns for which ordering is disabled
        var disabledOrderingColumnIndexArray = [];
        $(discoveredTable).find('th.disable-ordering').each(function () {
            disabledOrderingColumnIndexArray.push($(this).index());
        });

        // 2) Define a column to act as the default 'ordering' column (the class applied denotes this column and whether we order asc/desc)
        var defaultOrderingColumnIndex = defaultOrderingColumn.length !== 0 ? defaultOrderingColumn.index() : 0,
            defaultOrderType = defaultOrderingColumn.hasClass('default-order-column-desc') ? 'desc' : 'asc';

        // 3) Modify and return specific options as prepared in this function
        specificTableOptions = $.extend({}, {
            "columnDefs": [{
                "orderable": false,
                "targets": disabledOrderingColumnIndexArray
            }],
            "order": [[defaultOrderingColumnIndex, defaultOrderType]]
        }, specificTableOptions);

        return specificTableOptions;
    }

    // Configures the table header sortable icons (UI work essentially) based on the clicked table header element (and current 'sortable' state) 
    function handleTableHeaderUI(tableHeaderElementInScope) {
        if (tableHeaderElementInScope
            && tableHeaderElementInScope.length > 0
            // If sorting is disabled for this column by datatables.net do not proceed
            && !tableHeaderElementInScope.hasClass('sorting_disabled')) {
            // The element provided is set...next hide icons for any sibling th elements (just for this table) and show the relevant up/down arrow
            // based on the current sortable state of the row (add/remove internally check for the presence of the classes specified with no side effects).
            // Always cleaner to wipe down all classes and reapply just whats needed to avoid getting into an undesired state
            tableHeaderElementInScope.siblings('th').find('i').addClass('d-none');
            tableHeaderElementInScope.find('i')
                .removeClass('d-none fa-chevron-down fa-chevron-up')
                .addClass(tableHeaderElementInScope.hasClass('sorting_asc') ? 'fa-chevron-down' : 'fa-chevron-up');
        }
    }

    // Represents the core search function (table content filtered based on the input value from the configured search input, based on the events handled/setup within the module)
    function searchActiveTable(currentSearchInput) {
        var visibleActiveTableId;
        if (registeredSearchInputCount > 1) {
            // Multiple search inputs mean we discover the table via location (relative to the search input). More complex use cases can be added, as needed
            visibleActiveTableId = currentSearchInput.closest('.table-container').find('table:visible').attr('id');
        }
        else {
            // Obtain the active 'tab' data-tab value (or just the singular table) - this is used to infer the active data table
            visibleActiveTableId = $('table:visible').attr('id');
        }

        // From the active table, infer the active table stored within the module (has to be registered) and, if found, filter the results in the table based on the supplied search term
        if (visibleActiveTableId) {
            var activeTable = currentTables.find(function (table) {
                return table.table().node().id === visibleActiveTableId
            });

            if (activeTable) {
                activeTable.search(currentSearchInput.val()).draw();
            }
        }
    }

    // Publically Accessible Members

    // Configures the module for use. The 'Search Inputs' passed in are tied to configured events (when search is used/tabs are activated, etc.) and stock table options
    // are stored for shared use and consistency
    function configure(searchInputIds, sharedTableOptions) {
        // Setup shared table options
        sharedTableOptions = addTableOptionStockDefaults(sharedTableOptions);
        stockTableOptions = sharedTableOptions;

        // Next, configure event listeners
        setupEventListeners(searchInputIds);
    }

    // Registers a table, by id, within the module (the element must exist and must be a 'table' - module table options must also be available)
    function registerDataTableById(elementId) {
        if (elementId && stockTableOptions) {
            var discoveredTable = $(elementId);

            if (discoveredTable && discoveredTable.prop('tagName') === "TABLE") {
                var defaultOrderingColumn = getDefaultOrderingColumnFromTable(discoveredTable);
                    specificTableOptions = setupTableSpecificOptions(discoveredTable, defaultOrderingColumn);

                // Add the configured datatable to the module array for future use and hide generated elements (can be expanded, as needed - show/hide should ideally 
                // be based on options = future enhancement)
                currentTables.push(discoveredTable.DataTable(specificTableOptions));
                hideDataTablesGeneratedElements();
                handleTableHeaderUI(defaultOrderingColumn);
            }
        }
    }

    // Returns just the publically accessible members for use
    return {
        configure: configure,
        registerDataTableById: registerDataTableById
    }
})();