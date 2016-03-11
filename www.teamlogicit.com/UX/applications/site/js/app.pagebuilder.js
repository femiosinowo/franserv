define([
	'ektronjs',
	'Vendor/knockout/knockout',
	'./app.pagebuilder.resize',
	'./dropZoneService',
    './widgetService',
	'_i18n!./nls/labels'
], function ($, ko, ResizeViewModel, DropZoneService, WidgetService, labels) {

	function PageBuilderViewModel (settings) {
		/*
		  expected settings = {
			dialogClass: string,
			dialogsWrapperClass: string, 
			widgetMoveLocationClass: string,
			showMoveLocationsBodyClass: string
		  };
		*/
		var me = this;

		me.settings = settings;
		me.isPageBuilderPage = $('[data-ektron-pagebuilder-page="pagedata"]').length > 0 ? true : false;
		me.pageData = $('[data-ektron-pagebuilder-page="pagedata"]').length > 0 ?
				$.parseJSON($('[data-ektron-pagebuilder-page="pagedata"]').val()) :
                    {};
		me.isPageInEditMode = ko.computed(function () {
			var editMode = false;
			if ('undefined' !== typeof me.pageData.Mode && 'edit' === String(me.pageData.Mode).toLowerCase()) {
				editMode = true;
			}
			return editMode;
		});
		me.pageID = ko.computed(function () {
			return 'undefined' !== typeof (me.pageData.PageID) ? me.pageData.PageID : -1;
		});
		me.panel = {
			dialog: null,
			accordion: null,
			actions: ko.observable({
				Items: []
			}),
			propertiesGrid: ko.observableArray([]),
			resize: new ResizeViewModel(me),
			widgetList: ko.observable([])
		};
		me.widgetPath = '';
		me.sourceData = {
			id: ko.observable(),
			type: ko.observable(),
			moveActive: ko.observable(false),
			widget: ko.observable(),
			column: ko.observable(),
			dropZone: ko.observable()
		};
		me.sourceData.moveActive.subscribe(function (newValue) {
			if (newValue === true) {
				$('body').addClass(settings.showMoveLocationsBodyClass);
				var isMasterLayout = ("undefined" === me.pageData.IsMasterLayout) ? false : me.pageData.IsMasterLayout,
										isMasterZone = isMasterLayout ? true : false,
										widgets = $('[data-ux-pagebuilder-dropzone-data*=\'"IsMasterZone":' + isMasterZone + '\'] > [data-ux-pagebuilder-column-data] > ul > li'),
										columns = $('[data-ux-pagebuilder-dropzone-data*=\'"IsMasterZone":' + isMasterZone + '\'] > [data-ux-pagebuilder-column-data]'),
										emptyColumns = $('[data-ux-pagebuilder-dropzone-data*=\'"IsMasterZone":' + isMasterZone + '\'] > [data-ux-pagebuilder-column-data] > ul:not(:has(*))'),
										moveLocationHtml = '<li class="' + settings.widgetMoveLocationClass + '" title="' + labels.placeHere + '" onclick="Ektron.UX.apps.siteApp.moveLocationClick(this);"></li>',
										selectedWidget = '[data-ux-pagebuilder-widget-data].selected';

				widgets.before(moveLocationHtml).filter('[data-ux-pagebuilder-column-data] > ul > li:last-child').after(moveLocationHtml);
				emptyColumns.append(moveLocationHtml);

				// now that the LIs are in place, assign the correct index data point to each
				columns.each(function () {
					var column = $(this),
						widgetsMoveLocations = column.find('.' + settings.widgetMoveLocationClass);

					widgetsMoveLocations.each(function (index) {
						$(this).attr('data-ux-pagebuilder-widgetMoveLocation-index', index);
					});
				});

				// remove the two nodes we just added before and after the currently selected widget
				widgets.find(selectedWidget).closest('li').prev().remove();
				widgets.find(selectedWidget).closest('li').next().remove();
			}
			else {
				me.removeMoveLocations();
			}
		});
		me.targetData = {
			widget: ko.observable(),
			column: ko.observable(),
			dropZone: ko.observable()
		};
		me.dropZoneService = DropZoneService;
		me.widgetService = WidgetService;
		me.moveLocationClick = function (element) {
			var me = this,
				newWidgetIndex = $(element).data('uxPagebuilderWidgetmovelocationIndex');
			// set the target data
			me.targetData.widget({
				Index: (parseInt(newWidgetIndex, 10))
			});

			me.targetData.column($(element).closest('[data-ux-pagebuilder-column-data]').data('uxPagebuilderColumnData'));
			me.targetData.dropZone($(element).closest('[data-ux-pagebuilder-dropzone-data]').data('uxPagebuilderDropzoneData'));
			$('body').removeClass(settings.showMoveLocationsBodyClass);

			if (me.sourceData.column()) {
				// Move Existing Widget Instance within the Page
				me.widgetService.move(me.pageData, me.sourceData, me.targetData);
			}
			else {
				// Add New Widget to Page
				me.widgetService.add(me.pageData, me.sourceData, me.targetData);
			}
			me.clearSelected();
		},
		me.toggleEditMode = function (settings) {
			var editableWidget = $('[data-ux-pagebuilder-widget-edit-mode="true"]'),
				widgetBody = editableWidget.find('div.widgetBody'),
				minWidth = 425,
				widgetBodyWidth = widgetBody.outerWidth(),
				editWidgetDialog;

			$('form').append('<div id="uxPageBuilderEditableWidget"></div>');
			editWidgetDialog = $('#uxPageBuilderEditableWidget');
			editWidgetDialog.append(widgetBody).dialog({
				resizable: true,
				dialogClass: 'ektron-ux-dialog ux-app-siteApp-dialog',
				title: settings.title,
				minWidth: minWidth,
				minHeight: 465,
				closeOnEscape: false,
				modal: true,
				appendTo: 'form',
				close: function (event, ui) {
					editableWidget.append(widgetBody);
				},
				create: function (event, ui) {
					$('#uxPageBuilderEditableWidget').prev().children('button').remove();
					widgetBody.show();
				},
				open: function (event, ui) {
					var me = $(this);
					me.parent().css({ zIndex: '100000001' });
					$('.ui-widget-overlay').css({ zIndex: '100000001' });
					if (widgetBodyWidth > minWidth) {
						minWidth = widgetBodyWidth + parseInt(editWidgetDialog.css('padding-left'), 10) + parseInt(editWidgetDialog.css('padding-right'), 10);
						me.dialog('option', 'minWidth', minWidth);
					}
				}
			});
			editableWidget.append('<div style="display:block;min-height:100px;">&#160;</div>');
		},
		me.removeMoveLocations = function () {
			$('.' + settings.widgetMoveLocationClass).off('.uxSiteApp').remove();
			$('body').removeClass(settings.showMoveLocationsBodyClass);
			$('.ux-app-siteApp-widgetHeader .move.active').removeClass('active');
		};

		// move page builder's dialogs to our placeholder for theming
		$ektron('.ektronPageBuilderDialog').dialog("option", {
			appendTo: '.' + settings.dialogsWrapperClass,
			dialogClass: settings.dialogClass,
			closeText: '<span data-ux-icon="&#xe01a;" title="' + labels.close + '" aria-hidden="true" />'
		});
	}

	PageBuilderViewModel.prototype.clearSelected = function () {
		this.sourceData.id(null);
		this.sourceData.type(null);
		this.sourceData.moveActive(false);
		this.sourceData.widget(null);
		this.sourceData.column(null);
		this.sourceData.dropZone(null);

		this.targetData.widget(null);
		this.targetData.column(null);
		this.targetData.dropZone(null);

		// unbind and remove widgetMoveLocations
		$('.' + this.settings.widgetMoveLocationClass).off('.uxSiteApp').remove();
	};

	PageBuilderViewModel.prototype.setSelected = function (settings) {
		var me = this,
			s = {
				id: '',
				type: '',
				moveActive: false,
				widgetData: null,
				columnData: null,
				dropZoneData: null,
				callback: null
			},
			event;

		s = $.extend(s, settings);

		// clear old data
		this.clearSelected();

		this.sourceData.id(s.id);
		this.sourceData.type(s.type);
		this.sourceData.widget(s.widgetData);
		this.sourceData.column(s.columnData);
		this.sourceData.dropZone(s.dropZoneData);
		this.sourceData.moveActive(s.moveActive);
		if ('string' === typeof (s.callback) && s.callback.length > 0) {
			if (s.event !== null) {
				event = s.event;
			}
			eval(s.callback); //ignore jslint
		}
	};

	return PageBuilderViewModel;
});