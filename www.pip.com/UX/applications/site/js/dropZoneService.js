define([], function () {
    var __doPostBack = window.__doPostBack,
        DropZoneService = {
            add: function (pageData, sourceData, targetData) {
                var action = {
                    'Action': 'AddColumn'
                };
                __doPostBack(sourceData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            },
            move: function (pageData, sourceData, targetData) {
                var action = {
                    'Action': 'MoveColumn',
                    'OldWidgetLocation': {
                        'widgetTypeID': sourceData.column().originalIndex,
                        'dropZoneID': '0',
                        'ColumnID': sourceData.column().ID,
                        'OrderID': '0'
                    },
                    'NewWidgetLocation': {
                        'widgetTypeID': targetData.column().Index(),
                        'dropZoneID': sourceData.dropZone().ID,
                        'ColumnID': targetData.column().ID,
                        'OrderID': '0'
                    }
                };
                __doPostBack(sourceData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            },
            remove: function (pageData, sourceData, targetData) {
                var action = {
                    'Action': 'RemoveColumn',
                    'OldWidgetLocation': {
                        'widgetTypeID': '0',
                        'dropZoneID': '',
                        'ColumnID': '0',
                        'OrderID': '0'
                    },
                    'NewWidgetLocation': {
                        'widgetTypeID': '0',
                        'dropZoneID': sourceData.dropZone().ID,
                        'ColumnID': sourceData.column().ID,
                        'OrderID': '0'
                    }
                };
                __doPostBack(sourceData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            },
            resize: function (pageData, sourceData, targetData, settings) {
                settings.newunit = "pixels" === settings.newunit ? "px" : settings.newunit; //back end expects "px" not "pixels"
                var action = {
                    'Action': 'ResizeColumn',
                    'NewWidgetLocation': {
                        'dropZoneID': sourceData.dropZone().ID,
                        'ColumnID': sourceData.column().ID,
                        'Width': settings.newwidth,
                        'Unit': settings.newunit,
                        'CssClass': settings.cssclass,
                        'CssFramework': settings.cssframework
                    }
                };
                __doPostBack(sourceData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            },
            setMasterZone: function (pageData, sourceData, targetData) {
                var action = {
                    'Action': 'SetMasterZone',
                    'DropzoneInfo':
                    {
                        'dropZoneID': sourceData.dropZone().ID,
                        'isMaster': (!sourceData.dropZone().IsMasterZone)
                    }
                };
                __doPostBack(sourceData.dropZone().MarkupID, Ektron.JSON.stringify(action));
            }
        };
    return DropZoneService;
});