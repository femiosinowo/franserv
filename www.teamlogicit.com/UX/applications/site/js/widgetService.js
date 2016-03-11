define([], function () {
    var __doPostBack = window.__doPostBack,
        WidgetService = {
            add: function (pageData, sourceData, targetData) {
                var action = {
                    'Action': 'AddWidget',
                    'OldWidgetLocation': {
                        'isNested': false,
                        'nestedSortOrder': 0,
                        'widgetTypeID': parseInt(sourceData.widget().ID, 10), /* must be int */
                        'dropZoneID': '',
                        'ColumnID': 0,
                        'columnGuid': '00000000-0000-0000-0000-000000000000',
                        'OrderID': 0
                    },
                    'NewWidgetLocation': {
                        'isNested': false,
                        'nestedSortOrder': 0,
                        'widgetTypeID': parseInt(sourceData.widget().ID, 10),
                        'dropZoneID': targetData.dropZone().ID,
                        'ColumnID': targetData.column().ID,
                        'columnGuid': targetData.column().ColumnGUID,
                        'OrderID': targetData.widget().Index
                    }
                };
                __doPostBack(targetData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            },
            move: function (pageData, sourceData, targetData) {
                var action = {
                    'Action': 'MoveWidget',
                    'OldWidgetLocation': {
                        'isNested': false,
                        'nestedSortOrder': 0,
                        'widgetTypeID': parseInt(sourceData.widget().ID, 10), /* must be int */
                        'dropZoneID': sourceData.dropZone().ID,
                        'ColumnID': sourceData.column().ID,
                        'columnGuid': sourceData.column().ColumnGUID,
                        'OrderID': sourceData.widget().Index
                    },
                    'NewWidgetLocation': {
                        'isNested': false,
                        'nestedSortOrder': 0,
                        'widgetTypeID': parseInt(sourceData.widget().ID, 10), /* must be int */
                        'dropZoneID': targetData.dropZone().ID,
                        'ColumnID': targetData.column().ID,
                        'columnGuid': targetData.column().ColumnGUID,
                        'OrderID': targetData.widget().Index
                    }
                };
                __doPostBack(targetData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            }
        };
    return WidgetService;
});