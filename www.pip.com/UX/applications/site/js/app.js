define([
    'ektronjs',
    'Vendor/knockout/knockout',
    './app.mobilePreview',
    './app.pagebuilder',
    './app.content',
    '_i18n!./nls/labels',
    'UX/js/toolbar.switch',
    'UX/js/toolbar.buttonSet',
    'Vendor/Ektron/UX/js/widgets/ux.dialog',
    'Vendor/Ektron/UX/js/widgets/ux.accordion',
    './dropZoneService',
    './widgetService',
    './binder'
],
function ($, ko, MobilePreview, PageBuilderViewModel, ContentViewModel, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, DropZoneService, WidgetService, binder) {

    function getDefaultHref() {
        var locationArray = location.href.split('?');
        return locationArray[0];
    }

    function getState() {
        var state,
            defaultHref = getDefaultHref();

        state = sessionStorage.siteApp ? JSON.parse(sessionStorage.siteApp) : defaultState;
        return state;
    }

    var contentModeClass = 'ux-app-siteApp-contentMode',
        designModeClass = 'ux-app-siteApp-designMode',
        selectedContentClass = 'ux-app-siteApp-selectedContent',
        selectedDesignClass = 'ux-app-siteApp-selectedDesign',
        dialogClass = 'ektron-ux-dialog ux-app-siteApp-dialog',
        pageBuilderEditModeClass = 'pageBuilderEditMode',
        widgetMoveLocationClass = 'widgetMoveLocation',
        showMoveLocationsBodyClass = 'showMoveLocations',
        appName = 'siteApp',
        contentHeaderSelector = '.ux-app-siteApp-contentHeaderWrapper',
        pageBuilderHeaderSelector = '[data-ux-pagebuilder-column-data] :first-child',
        dialogsWrapperClass = 'ux-app-siteApp-dialogs',
        contentButtonId = 'uxSiteAppContent',
        designButtonId = 'uxSiteAppDesign',
        defaultState = {
            editType: contentButtonId,
            isEditMode: false,
            href: getDefaultHref(),
            pageBuilderPageID: -1,
            isPageBuilderDialogOpen: false,
            pageBuilderAccordionIndex: 0
        },
        initialState = getState(),
        allowUpdate = false;

    function Application(settings) {
        var me = this;
        // app properties
        me.labels = labels;
        me.resourceLoader = settings.resourceLoader;
        me.appWindow = settings.appWindow;
        me.toolbar = settings.toolbar;
        me.mobilePreview = null;
        me.editSwitch = null;
        me.modeButtonSet = null;
        me.namespace = 'siteApp';

        me.content = new ContentViewModel();

        me.content.hasContentItems.subscribe(function (newValue) {
            if (newValue === false && !me.pageBuilder.isPageBuilderPage) {
                me.editSwitch.isVisible(false);
                me.modeButtonSet.buttons()[0].isVisible(false);
            }
            else {
                me.editSwitch.isVisible(true);
                me.modeButtonSet.buttons()[0].isVisible(true);
            }
        });

        me.pageBuilder = new PageBuilderViewModel({
            dialogClass: dialogClass,
            dialogsWrapperClass: dialogsWrapperClass,
            widgetMoveLocationClass: widgetMoveLocationClass,
            showMoveLocationsBodyClass: showMoveLocationsBodyClass
        });
        me.panelsToUpdate = ko.observable();
        me.beginPostBackHandler = function (sender, args) {
            var panelsToUpdate = $.isArray(args._updatePanelsToUpdate) ? args._updatePanelsToUpdate : [];
            if (panelsToUpdate.length === 0) {
                panelsToUpdate = sender._updatePanelClientIDs;
            }
            me.panelsToUpdate(panelsToUpdate);
            ko.utils.arrayForEach(panelsToUpdate, function (panel) {
                var currentPanel = document.getElementById(panel.replace(/\$/g, '_'));
                $(currentPanel).find(contentHeaderSelector + ', ' + pageBuilderHeaderSelector).each(function (index, Element) {
                    ko.cleanNode(Element);
                });
            });
        };
        me.endPostBackHandler = function (sender, args) {
            var contentSelector = '[data-ektron-editors-menu]';
            me.content.hasContentItems($(contentSelector).length > 0 ? true : false);
            ko.utils.arrayForEach(me.panelsToUpdate(), function (panel) {
                me.rebindHandler({
                    'type': 'editorsMenu',
                    'selector': contentSelector
                });
            });
        };
        me.rebindHandler = function (settings) {
            var s = {
                type: '', // 'column'/'widget'
                selector: '' // .NET ClientID
            };

            s = $.extend(s, settings);

            switch (s.type) {
            case 'column':
                bindColumnData(me, s.selector);
                break;
            case 'widget':
                bindWidgetData(me, s.selector);
                break;
            case 'editorsMenu':
                createContentHeaders(me, s.selector);
                break;
            default:
                // do nothing
                break;
            }
        };
    }

    // app methods
    Application.prototype.onOpen = function () {
        var me = this,
            appTemplate,
            carouselTemplate;

        me.resourceLoader.load({
            files: ['app.min', 'app.pagebuilder.min', 'app.mobilePreview.min'],
            type: me.resourceLoader.Types.css
        });

        me.resourceLoader.load({
            files: ['app', 'app.carousel'],
            type: me.resourceLoader.Types.html,
            callback: function (app, carousel) {
                appTemplate = app;
                carouselTemplate = carousel;

                me.resourceLoader.load({
                    files: ['app.linkList', 'app.propertiesGrid', 'app.widgetList'],
                    type: me.resourceLoader.Types.html,
                    reusable: true,
                    callback: function () {
                        $(me.appWindow.getElement()).append(appTemplate);
                        addEditControls(me);
                        me.content.hasContentItems($('[data-ektron-editors-menu]').length > 0 ? true : false);
                        mobilePreviewInit(me, appTemplate, carouselTemplate);
                        if (me.content.hasContentItems() || me.pageBuilder.isPageBuilderPage) {
                            createContentHeaders(me);
                        }
                        createContentDialog(me); // always create the dialog so it exists if content is brought in later.
                        if (me.pageBuilder.isPageBuilderPage) {
                            createPageBuilderDialog(me);
                            bindColumnData(me);
                            bindWidgetData(me);
                        }
                        setInitialAppState(me);
                    }
                });
            }
        });

        if ('undefined' === typeof (Ektron.UX.apps.siteApp)) {
            Ektron.UX.apps.siteApp = {
                beginPostBackHandler: function (sender, args) {
                    me.beginPostBackHandler(sender, args);
                },
                endPostBackHandler: function (sender, args) {
                    me.endPostBackHandler(sender, args);
                },
                PageBuilder: {
                    Widget: {
                        toggleEditMode: function (settings) {
                            me.pageBuilder.toggleEditMode(settings);
                        }
                    }
                },
                moveLocationClick: function (element) {
                    me.pageBuilder.moveLocationClick(element);
                },
                rebindHandler: function (settings) {
                    me.rebindHandler(settings);
                },
                onSelectChange: function () {
                    me.pageBuilder.panel.resize.onSelectChange();
                },
                showResize: function (event) {
                    me.pageBuilder.panel.resize.show(event);
                },
                hideResize: function () {
                    me.pageBuilder.panel.resize.hide();
                },
                resize: function () {
                    me.pageBuilder.panel.resize.resizeColumn();
                }
            };

            // bind to edit in context started event from Aloha Editor to hide the content dialog
            $(document).on('editInContextStarted.uxSiteApp', function (event) {
                me.editSwitch.isVisible(false);
                me.modeButtonSet.isVisible(false);
                $(me.content.contentDialog.element()).dialog('close');
            });

            // bind to edit in context complete event from Aloha Editor to recreate content headers
            $(document).on('editInContextComplete.uxSiteApp', function (event, activeEditable) {
                me.editSwitch.isVisible(true);
                me.modeButtonSet.isVisible(true);
                me.rebindHandler({
                    type: 'editorsMenu',
                    selector: activeEditable
                });
            });
        }
    };

    Application.prototype.onClose = function () {
        // Boilerplate knockout cleanup
        var me = this,
            element,
            state = getState();

        if (me.mobilePreview !== null) {
            me.mobilePreview.close();
        }

        element = $(me.appWindow.getElement()).children()[0];
        if ('undefined' !== typeof (element)) {
            ko.removeNode(element);
        }

        // app specific clean up
        element = $('#ux.toolbar.button, #ux.toolbar.popover, #ux.toolbar.switch, #ux.toolbar.buttonset, #app.contentHeader').each(function (index, domElement) {
            if (typeof (domElement) !== 'undefined') {
                ko.removeNode(domElement);
            }
        });
        // unbind jcarousel plug-in's resize event since the carousel is not visible when dashboard app is open.
        $(window).off('resize.jcarousel').off('.uxSiteApp');

        if (me.content.contentDialog) {
            ko.removeNode(me.content.contentDialog.element());
        }
        if (me.pageBuilder.isPageBuilderPage && me.pageBuilder.panel.dialog) {
            ko.removeNode(me.pageBuilder.panel.dialog.element());
        }
        // ensure the state is the same as it was before the onClose process began
        updateState(me, state);
        // prevent any more updates to state
        allowUpdate = false;
    };

    Application.prototype.openContentDialogue = function (content, menuData) {
        var me = this,
            index = 0,
            link,
            item,
            tempPanels,
            dialogObj = $(me.content.contentDialog.element()),
            contentAccordion = $(me.content.contentDialogAccordion.element());

        me.content.contentDialogAccordionLinkList(menuData);

        dialogObj.dialog({ title: menuData.Name });
        dialogObj.dialog('open');
        // refresh the heights of the panels just to be safe
        if (contentAccordion.is('.ui-accordion')) {
            contentAccordion.accordion('refresh');
        }
        // remove the selected class from any other content items and mark the current content item as selected
        clearSelectedContent();
        content.addClass(selectedContentClass);
    };

    // helper functions
    function addEditControls(me) {
        var state = getState(),
            buttonsetButtonsArray = [],
            locationArray = location.href.split('?');

        if (me.pageBuilder.isPageInEditMode() && (locationArray[0] !== state.href || me.pageBuilder.pageID() !== state.pageBuilderPageID)) {
            // if a pageBuilder page is in edit mode, but there is no state for this page,
            // it is likely a newly created PB page opened in edit mode 
            // (this is an option from the workarea)
            state.isEditMode = true;
            state.editType = designButtonId;
            state.href = locationArray[0];
            state.pageBuilderPageID = me.pageBuilder.pageID();
            updateState(me, state);
        }

        me.editSwitch = new ToolbarSwitch({
            id: 'editSwitch',
            onLabel: labels.editSwitchOnLabel,
            offLabel: labels.editSwitchOffLabel,
            checked: state.isEditMode ? state.isEditMode : false,
            cssClass: 'editModeToggle',
            visible: (me.content.hasContentItems() || me.pageBuilder.isPageBuilderPage) ? true : false,
            click: function (data, event) {
                me.modeButtonSet.disabled(!(me.modeButtonSet.disabled()));
                // remove the highlight class on click in case user switched back to view mode
                highlightClass(me);
                if (true === me.modeButtonSet.disabled()) {
                    $(me.content.contentDialog.element()).dialog('close');
                    if (me.pageBuilder.isPageBuilderPage) {
                        $(me.pageBuilder.panel.dialog.element()).dialog('close');
                        me.pageBuilder.panel.resize.hide();
                    }
                    me.modeButtonSet.options.cssClass('editModesButtonSet');
                }
                else {
                    if (designButtonId === me.modeButtonSet.val()) {
                        $(me.pageBuilder.panel.dialog.element()).dialog('open');
                    }
                }
                $('body').toggleClass('ux-app-siteApp-edit');
                updateState(me);
            }
        });
        me.toolbar.addSwitch(me.editSwitch);

        buttonsetButtonsArray.push({
            id: contentButtonId,
            text: labels.contentLabel,
            checked: designButtonId !== state.editType,
            visible: me.content.hasContentItems() ? true : false,
            click: function () {
                highlightClass(me);
                if (me.pageBuilder.isPageBuilderPage) {
                    $(me.pageBuilder.panel.dialog.element()).dialog('close');
                    me.pageBuilder.panel.resize.hide();
                }
                me.modeButtonSet.options.cssClass('editModesButtonSet');
                updateState(me);
            }
        });

        //only add design button if this is a pagebuilder page
        if (me.pageBuilder.isPageBuilderPage) {
            buttonsetButtonsArray.push({
                id: designButtonId,
                text: labels.designLabel,
                checked: designButtonId === state.editType,
                click: function () {
                    highlightClass(me);
                    $(me.content.contentDialog.element()).dialog('close');
                    if (me.pageBuilder.isPageBuilderPage) {
                        $(me.pageBuilder.panel.dialog.element()).dialog('open');
                    }
                    updateState(me);
                }
            });
        }

        me.modeButtonSet = new ToolbarButtonSet({
            id: 'editMode',
            buttons: buttonsetButtonsArray,
            type: 'radio',
            cssClass: 'editModesButtonSet',
            disabled: true
        });
        me.toolbar.addButtonset(me.modeButtonSet);
    }

    function bindWidgetData(me, optionalScopeLimiter) {
        var embeddedWidgets,
            scopeElement;

        if (optionalScopeLimiter) {
            scopeElement = $(optionalScopeLimiter);
        }
        else {
            scopeElement = $('body');
        }

        embeddedWidgets = scopeElement.find('[data-ux-pagebuilder-widget-data]');

        embeddedWidgets.each(function (index, element) {
            var widget = $(this),
                widgetData,
                widgetHeader = widget.find('.widgetHeader'),
                columnData = widget.closest('[data-ux-pagebuilder-column-data]').data('uxPagebuilderColumnData'),
                dropZoneData = widget.closest('[data-ux-pagebuilder-dropzone-data]').data('uxPagebuilderDropzoneData');
            widgetData = widget.data('uxPagebuilderWidgetData');

            widgetData.labels = me.labels;
            widgetData.isSelected = ko.computed(function () {
                return me.pageBuilder.sourceData.id() === widgetData.ClientID && me.pageBuilder.sourceData.type() === 'widget';
            });
            widgetData.isSelected.subscribe(function (newValue) {
                var selectedClass = 'selected';
                if (newValue) {
                    widget.addClass(selectedClass);
                }
                else {
                    widget.removeClass(selectedClass);
                }
            });
            widgetData.select = function (item, event) {
                if (widgetData.isSelected()) {
                    me.pageBuilder.clearSelected();
                }
                else {
                    me.pageBuilder.setSelected({
                        id: widgetData.ClientID,
                        type: 'widget',
                        moveActive: false,
                        widgetData: widgetData,
                        columnData: columnData,
                        dropZoneData: dropZoneData,
                        callback: null
                    });
                }
                event.stopPropagation();
                return false;
            };
            widgetData.isMoveActive = ko.computed(function () {
                return me.pageBuilder.sourceData.id() === widgetData.ClientID && me.pageBuilder.sourceData.moveActive() === true;
            });
            widgetData.moveClass = ko.computed(function () {
                // need to return either 'move' or 'move active'
                if (widgetData.isMoveActive()) {
                    return 'move active';
                }
                return 'move';
            });
            ko.utils.arrayForEach(widgetData.Actions, function (item) {
                var iconHTML;
                item.fireAction = function (data, event) {
                    var moveActive = false,
                        widgetId = widgetData.ClientID;
                    // update moveActive state
                    switch (this.Action.toLowerCase()) {
                    case 'move':
                        if (me.pageBuilder.sourceData.id() === widgetData.ClientID) {
                            moveActive = !(me.pageBuilder.sourceData.moveActive());
                        }
                        else {
                            moveActive = true;
                        }
                        break;
                    case 'remove':
                        widgetId = null;
                        break;
                    default:
                        break;
                    }

                    me.pageBuilder.setSelected({
                        id: widgetId,
                        type: 'widget',
                        moveActive: moveActive,
                        widgetData: widgetData,
                        columnData: columnData,
                        dropZoneData: dropZoneData,
                        callback: item.Callback
                    });
                    event.stopPropagation();
                    return false;
                };
                switch (item.Action.toLowerCase()) {
                case 'launchhelp':
                    iconHTML = '<span data-ux-icon="&#xe02b;" />';
                    break;
                case 'edit':
                    iconHTML = '<span data-ux-icon="&#xe011;" />';
                    break;
                case 'remove':
                    iconHTML = '<span data-ux-icon="&#xe012;" />';
                    break;
                case 'move':
                    iconHTML = '<span data-ux-icon="&#xe028;" />';
                    break;
                default:
                    iconHTML = '';
                    break;
                }
                item.icon = iconHTML;
            });
            ko.applyBindings(widgetData, widgetHeader[0]);
        });
    }

    function bindColumnData(me, optionalScopeLimiter) {
        var columns,
            scopeElement;

        if (optionalScopeLimiter) {
            scopeElement = $(optionalScopeLimiter);
        }
        else {
            scopeElement = $('body');
        }

        columns = scopeElement.find('[data-ux-pagebuilder-column-data]');

        columns.each(function (index, element) {
            var column = $(this),
                columnData = column.data('uxPagebuilderColumnData'),
                dropZone = column.parent(),
                dropZoneData = dropZone.data('uxPagebuilderDropzoneData'),
                columnId = dropZoneData.ID + '_' + columnData.ID,
                columnHeader = column.find(':first-child'),
                siblingColumns = column.parent().children(),
                tempArray = [];//siblings('[data-ux-pagebuilder-column-data]');

            columnData.positionOptions = ko.observableArray();
            siblingColumns.each(function (index) {
                columnData.positionOptions.push({ index: index, text: (index + 1).toString() });
            });
            columnData.label = labels.column;
            columnData.originalIndex = parseInt(ko.utils.unwrapObservable(columnData.Index), 10);

            tempArray = columnData.positionOptions();

            columnData.Index = ko.observable(columnData.positionOptions()[columnData.originalIndex].index);
            columnData.Index.subscribe(function (newValue) {
                var pageBuilder = me.pageBuilder;

                pageBuilder.targetData.column({
                    Index: ko.observable(newValue)
                });

                pageBuilder.sourceData.dropZone(dropZoneData);
                pageBuilder.sourceData.column(columnData);
                pageBuilder.dropZoneService.move(pageBuilder.pageData, pageBuilder.sourceData, pageBuilder.targetData);
            });
            columnData.isSelected = ko.computed(function () {
                var columnSameGuid = true;
                if (me.pageBuilder.sourceData.column()) {
                    columnSameGuid = me.pageBuilder.sourceData.column().ColumnGUID === columnData.ColumnGUID;
                }

                return (me.pageBuilder.sourceData.id() === columnId && columnSameGuid) && me.pageBuilder.sourceData.type() === 'column';
            });
            columnData.isSelectedSibling = ko.computed(function () {
                var dropZoneId = me.pageBuilder.sourceData.dropZone() ? me.pageBuilder.sourceData.dropZone().ID : -1;
                return dropZoneId === dropZoneData.ID && !(columnData.isSelected()) && me.pageBuilder.sourceData.type() === 'column';
            });
            columnData.mouseoverHeader = function (data, event) {
                $(event.currentTarget).parent().addClass('darken');
            },
            columnData.mouseoutHeader = function (data, event) {
                $(event.currentTarget).parent().removeClass('darken');
            },
            columnData.selectedClass = ko.computed(function () {
                var cssClass = '';
                if (columnData.isSelected()) {
                    cssClass = 'selected';
                }
                else if (columnData.isSelectedSibling()) {
                    cssClass = 'selectedSibling';
                }
                return cssClass;
            });
            columnData.selectedClass.subscribe(function (newValue) {
                column.removeClass('selected').removeClass('selectedSibling').addClass(newValue);
            });
            columnData.select = function (item, event) {
                var selectedData = {
                    id: columnId,
                    type: 'column',
                    moveActive: false,
                    widgetData: null,
                    columnData: columnData,
                    dropZoneData: dropZoneData,
                    callback: null
                };
                if (columnData.isSelected()) {
                    me.pageBuilder.clearSelected();
                }
                else {
                    me.pageBuilder.setSelected(selectedData);
                }
                event.stopPropagation();
                $(window).trigger('selectColumn.uxSiteApp', [selectedData]);
                return false;
            };

            ko.utils.arrayForEach(columnData.Actions, function (item) {
                var iconHTML;

                item.fireAction = function (data, event) {
                    me.pageBuilder.setSelected({
                        id: columnId,
                        type: 'column',
                        moveActive: false,
                        widgetData: null,
                        columnData: columnData,
                        dropZoneData: dropZoneData,
                        callback: item.Callback,
                        event: event
                    });
                    event.stopPropagation();
                    return false;
                };
                switch (item.Action.toLowerCase()) {
                case 'addcolumn':
                    iconHTML = '<span data-ux-icon="&#xe010;" />';
                    break;
                case 'removecolumn':
                    iconHTML = '<span data-ux-icon="&#xe012;" />';
                    break;
                case 'resizecolumn':
                    iconHTML = '<span data-ux-icon="&#xe009;" />';
                    break;
                case 'setmasterzone':
                    iconHTML = '<span data-ux-icon="&#xe00b;" />';
                    break;
                case 'unsetmasterzone':
                    iconHTML = '<span data-ux-icon="&#xe01c;" />';
                    break;
                default:
                    iconHTML = '';
                    break;
                }
                item.icon = iconHTML;
            });
            ko.applyBindings(columnData, columnHeader[0]);
        });
    }

    function clearSelectedContent() {
        $('.' + selectedContentClass).removeClass(selectedContentClass);
    }

    function clearSelectedDesigns() {
        $('.' + selectedDesignClass).removeClass(selectedDesignClass).removeClass(pageBuilderEditModeClass);
    }

    function createContentDialog(me) {
        placeDialogWrapper();

        me.content.contentDialog = new Dialog({
            options: {
                appendTo: '.' + dialogsWrapperClass,
                autoOpen: false,
                close: function (event, ui) {
                    clearSelectedContent();
                },
                dialogClass: dialogClass,
                minHeight: '85px',
                position: { my: 'bottom', at: 'center', of: window },
                resizable: false
            },
            title: '',
            html: '<div data-bind="template: { name: \'ux-accordion\', data: content.contentDialogAccordion }" class="ux-siteApp-contentAccordion"></div>'
        });

        me.content.contentDialogAccordion = new Accordion({
            options: {
                heightStyle: 'auto'
            },
            panelsArray: [
                {
                    title: labels.actions + '',
                    html: '<div data-bind="template: {name: \'app.linkList\', data: content.contentDialogAccordionLinkList }" class="ux-siteApp-contentLinkList"></div>'
                }
            ]
        });

        binder.applyBindings(me, '.ux-app-site .contentEditing .ux-siteApp-contentDialogTemplateBinder'); // binds the dialog

        binder.applyBindings(me, '.ux-siteApp-contentAccordion'); // binds the accordion
        binder.applyBindings(me, '.ux-siteApp-contentLinkList'); // binds the linklist
    }

    function createPageBuilderDialog(me) {
        var pageActionData,
            prop,
            propertiesData,
            propertiesDataArray = [],
            widgetListData,
            index = 0,
            state = initialState;

        placeDialogWrapper();

        me.pageBuilder.panel.dialog = new Dialog({
            options: {
                appendTo: '.' + dialogsWrapperClass,
                autoOpen: false,
                close: function (event, ui) {
                    if ($(this).find('.widgetToken.selected').length > 0) {
                        me.pageBuilder.clearSelected();
                    }
                    if ('uxSiteAppDesign' === me.modeButtonSet.val()) {
                        me.modeButtonSet.options.cssClass('editModesButtonSet designGlow');
                    }
                    else {
                        me.modeButtonSet.options.cssClass('editModesButtonSet');
                    }
                    updateState(me);
                },
                position: {
                    my: 'right top',
                    at: 'right bottom',
                    of: $('.ektron-ux .toolbarWrapper')
                },
                minHeight: 85,
                dialogClass: dialogClass,
                open: function (event, ui) {
                    me.modeButtonSet.options.cssClass('editModesButtonSet');
                    $(me.pageBuilder.panel.accordion.element()).accordion('refresh');
                    updateState(me);
                },
                resizable: false
            },
            title: me.labels.pagebuilder,
            html: '<div data-bind="template: { name: \'ux-accordion\', data: pageBuilder.panel.accordion }" class="ux-siteApp-designAccordion"></div>'
        });

        pageActionData = $.parseJSON($('[data-ektron-pagebuilder-page="menu"]').val());
        propertiesData = $.parseJSON($('[data-ektron-pagebuilder-page="properties"]').val());
        widgetListData = $.parseJSON($('[data-ektron-pagebuilder-widget-data="true"]').val());

        ko.utils.arrayForEach(widgetListData, function (widget) {
            widget.ClientID = 'widgetTray_' + widget.ID;
            widget.isSelected = ko.computed(function () {
                return me.pageBuilder.sourceData.id() === widget.ClientID && me.pageBuilder.sourceData.type() === 'widget';
            });
            widget.selectedClass = ko.computed(function () {
                return widget.isSelected() ? 'widgetToken selected' : 'widgetToken';
            });
            widget.select = function (item, event) {
                if (widget.isSelected()) {
                    me.pageBuilder.clearSelected();
                }
                else {
                    me.pageBuilder.setSelected({
                        id: widget.ClientID,
                        type: 'widget',
                        moveActive: true,
                        widgetData: widget,
                        columnData: null,
                        dropZoneData: null,
                        callback: null
                    });
                }
                event.stopPropagation();
                return false;
            };
            widget.isMoveActive = ko.computed(function () {
                return me.pageBuilder.sourceData.id() === widget.ClientID && me.pageBuilder.sourceData.moveActive() === true;
            });
        });
        me.pageBuilder.widgetPath = $('[data-ektron-widgets-path]').data('ektronWidgetsPath');

        // map page properties to an array
        for (prop in propertiesData) {
            propertiesDataArray.push({
                key: prop,
                value: propertiesData[prop]
            });
        }

        me.pageBuilder.panel.actions(pageActionData);
        me.pageBuilder.panel.propertiesGrid(propertiesDataArray);
        me.pageBuilder.panel.widgetList(widgetListData);
        
        me.pageBuilder.panel.accordion = new Accordion({
            options: {
                collapsible: true,
                heightStyle: 'content',
                activate: function (event, ui) {
                    updateState(me);
                },
                active: state.pageBuilderAccordionIndex
            },
            panelsArray: [
                {
                    title: (labels.pageAction + ''),
                    html: '<div data-bind="template: {name: \'app.linkList\', data: pageBuilder.panel.actions }" class="ux-siteApp-pageBuilderLinkList"></div>'
                },
                {
                    title: (labels.properties + ''),
                    html: '<div data-bind="template: {name: \'app.propertiesGrid\', data: pageBuilder.panel.propertiesGrid }" class="ux-siteApp-pageBuilderPropertiesGrid"></div>'
                }
            ]
        });

        if (me.pageBuilder.isPageInEditMode()) {
            me.pageBuilder.panel.accordion.panels().push({
                title: (labels.widgets + ''),
                html: '<div data-bind="template: {name: \'app.widgetList\', data: pageBuilder.panel.widgetList }" class="ux-siteApp-pageBuilderWidgetList"></div>'
            });
        }

        binder.applyBindings(me, '.ux-app-site .contentEditing .ux-siteApp-pageBuilderDialogTemplateBinder'); // binds the dialog
        binder.applyBindings(me, '.ux-siteApp-designAccordion'); // binds the accordion
        binder.applyBindings(me, '.ux-siteApp-pageBuilderLinkList'); // binds the linklist
        binder.applyBindings(me, '.ux-siteApp-pageBuilderPropertiesGrid'); // binds the properties grid
        binder.applyBindings(me, '.ux-siteApp-pageBuilderWidgetList'); // binds the widget list
    }

    function createContentHeaders(me, optionalScopeLimiter) {
        var content,
            contentData,
            contentWidth,
            contentZindex,
            scopeElement;

        me.resourceLoader.load({
            type: me.resourceLoader.Types.html,
            files: ['app.contentHeader'],
            reusable: true,
            callback: function (template) {
                if (optionalScopeLimiter) {
                    scopeElement = $(optionalScopeLimiter).parent();
                }
                else {
                    scopeElement = $('body');
                }

                scopeElement.find('[data-ektron-editors-menu]').each(function (index) {
                    var content = $(this),
                        contentData = content.data('ektron-editors-menu'),
                        contentZindex = ('auto' === content.css('zIndex')) ? 0 : content.css('zIndex'),
                        existingHeaders = $(this).find('.ux-app-siteApp-contentHeader');
                    // remove any previous header templates
                    if (existingHeaders.length > 0) {
                        existingHeaders.each(function (index) {
                            ko.removeNode(this);
                        });
                    }

                    var templateObj = $(template);
                    templateObj.find('.title').html(contentData.Name);
                    content.prepend(templateObj);

                    content.find('.ux-app-siteApp-contentHeader [data-ux-icon]').on('click.uxSiteApp', function () {
                        var content,
                            contentData;

                        content = $(this).closest('[data-ektron-editors-menu]');
                        contentData = content.data('ektron-editors-menu');
                        me.openContentDialogue(content, contentData);
                    });
                });
            }
        });
    }

    function highlightClass(me) {
        var body = $('body');
        if (me.editSwitch.checked()) {
            switch (me.modeButtonSet.val()) {
            case 'uxSiteAppContent':
                body.removeClass(designModeClass);
                body.addClass(contentModeClass);
                me.pageBuilder.removeMoveLocations();
                $('[data-ux-pagebuilder-widget-data], [data-ux-pagebuilder-column-data]').removeClass('selected');
                break;
            case 'uxSiteAppDesign':
                body.removeClass(contentModeClass);
                body.addClass(designModeClass);
                if (me.pageBuilder.isPageInEditMode()) {
                    body.addClass(pageBuilderEditModeClass);
                }
                break;
            default:
                // do nothing
                break;
            }
        }
        else {
            body.removeClass(designModeClass);
            body.removeClass(contentModeClass);
            me.pageBuilder.removeMoveLocations();
            $('[data-ux-pagebuilder-widget-data], [data-ux-pagebuilder-column-data]').removeClass('selected');
        }
    }

    function mobilePreviewInit(me, app, carousel) {
        // clear the contents of the application window
        me.appWindow.clear();
        // initialize mobile preview
        me.mobilePreview = new MobilePreview({
            toolbar: me.toolbar,
            resourceLoader: me.resourceLoader,
            appWindow: me.appWindow,
            previewTemplate: app,
            carouselTemplate: carousel
        });
        me.mobilePreview.init();
    }

    function updateState(me, optionalState) {
        var state = initialState,
            locationArray = [];

        if (allowUpdate) {
            if ('undefined' === typeof optionalState) {
                state.isEditMode = me.modeButtonSet.disabled() ? false : true;
                state.editType = me.modeButtonSet.val();
                locationArray = location.href.split('?');
                state.href = locationArray[0];

                //defaults
                state.pageBuilderPageID = 0;
                state.isPageBuilderDialogOpen = false;
                state.pageBuilderAccordionIndex = 0;            

                //if pagebuilder page
                if (null !== me.pageBuilder.pageID()) {
                    state.pageBuilderPageID = me.pageBuilder.pageID();
                }
                if (null !== me.pageBuilder.panel && null !== me.pageBuilder.panel.dialog) {
                    state.isPageBuilderDialogOpen = $(me.pageBuilder.panel.dialog.element()).dialog('isOpen') ? true : false;
                    state.pageBuilderAccordionIndex = isNaN($(me.pageBuilder.panel.accordion.element()).accordion('option', 'active')) ? state.pageBuilderAccordionIndex : $(me.pageBuilder.panel.accordion.element()).accordion('option', 'active');
                }
            }
            else {
                state = optionalState;
            }
            sessionStorage[appName] = JSON.stringify(state);
        }
    }

    function setInitialAppState(me) {
        var state = initialState;

        if (me.content.hasContentItems || me.pageBuilder.isPageBuilderPage) {
            if (state.href === getDefaultHref()) {
                // only do stuff if we have state for the current page
                if (state.isEditMode) {
                    me.editSwitch.checked(true);
                }
                if (designButtonId === state.editType && me.editSwitch.checked()) {
                    if (me.pageBuilder.isPageBuilderPage && state.isPageBuilderDialogOpen === true) {
                        $(me.pageBuilder.panel.dialog.element()).dialog('open');
                    }
                    else if (me.pageBuilder.isPageBuilderPage) {
                        $(me.pageBuilder.panel.dialog.element()).dialog('close');
                    }
                }
            }
        }
        allowUpdate = true;
    }

    function placeDialogWrapper() {
        var dialogWrapper = $('.' + dialogsWrapperClass);

        if (dialogWrapper.length < 1) {
            $('body form').append('<div class="ektron-ux-reset ektron-ux-UITheme ' + dialogsWrapperClass + '" />');
        }
    }

    return Application;
}
);