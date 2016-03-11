define(['ektronjs',
	'Vendor/knockout/knockout'
], function ($, ko) {

	function ResizeViewModel(PageBuilder) {
		// methods
		this.show = function (event) {
			var select = $('div.ux-app-siteApppageBuilder-setSizeTemplate select.ColumnTypeDropDown'),
				data = this.getOriginalValues();

			//get select option id of width value type
			data.id = this.getId(data);

			//set select box option to width valye type
			select.val(data.id);

			//show form with width value
			this.toggleForm(data);

			//show dialog
			$('div.ux-app-siteApppageBuilder-setSizeTemplate').show().position({
				my: 'right top',
				at: 'right bottom',
				of: $(event.target)
			});

			$(".ux-app-siteApppageBuilder-setSizeTemplate .framework").accordion("option", "active", 0);
		};

		this.hide = function () {
			$('div.ux-app-siteApppageBuilder-setSizeTemplate').hide();
		};

		this.getId = function (data) {
			var type = 0,
				unitName = "";

			if ("pixels" === data.newunit.toLowerCase()) {
				unitName = data.newunit;
				type = "0"; //set in pagebuilder.config
			}
			if ("em" === data.newunit.toLowerCase()) {
				unitName = data.newunit;
				type = "1"; //set in pagebuilder.config
			}
			if ("percent" === data.newunit.toLowerCase()) {
				unitName = data.newunit;
				type = "2"; //set in pagebuilder.config
			}
			if ("custom" === data.newunit.toLowerCase() || "3" === data.newunit) {
				unitName = data.cssframework;
				type = "3"; //set in pagebuilder.config
			}

			return unitName + "-_-" + type;
		};

		this.getOriginalValues = function () {
			return {
				'newwidth': PageBuilder.sourceData.column().Width,
				'newunit': PageBuilder.sourceData.column().UnitName,
				'cssclass': PageBuilder.sourceData.column().CssClass,
				'cssframework': PageBuilder.sourceData.column().CssFramework
			};
		};

		this.getSelectValues = function () {
			var resizePanel = $('div.ux-app-siteApppageBuilder-setSizeTemplate'),
				select = $('div.ux-app-siteApppageBuilder-setSizeTemplate select.ColumnTypeDropDown'),
				id = select.val(),
				sizeUnit = "",
				cssframework = "",
				cssclass = "",
				settings = {},
				unitName = id.split('-_-')[0],
				unitType = parseInt(id.split('-_-')[1], 10),
				width = 0,
				framework = null,
				options = null;

			if (unitType > 2) {
				//framework
				cssframework = unitName;
				$("#" + id).find("select option:selected").each(function (i) {
					var value = $(this).attr("value");
					if ("" !== value) {
						cssclass += " " + value;
					}
					cssclass = cssclass.replace(/^\s+|\s+$/g, '');
				});
				sizeUnit = "3";
			} else {
				//size
				sizeUnit = unitName;
				width = resizePanel.find('div#' + id + ' input.newwidth').val();
			}

			settings = {
				'newwidth': width,
				'newunit': sizeUnit,
				'cssclass': cssclass,
				'cssframework': cssframework
			};
			return settings;
		};

		this.onSelectChange = function () {
			var select = $('div.ux-app-siteApppageBuilder-setSizeTemplate select.ColumnTypeDropDown'),
				data = this.getSelectValues();

			data.id = this.getId(data);

			this.toggleForm(data);
		};

		this.resizeColumn = function () {
			var settings = this.getSelectValues();
			PageBuilder.dropZoneService.resize(PageBuilder.pageData, PageBuilder.sourceData, PageBuilder.targetData, settings);
			this.hide();
		};

		this.toggleForm = function (data) {
			//initialize vars with values set on column
			var dialogContent = $('div.ux-app-siteApppageBuilder-setSizeTemplate'),
				select = $('div.ux-app-siteApppageBuilder-setSizeTemplate select.ColumnTypeDropDown'),
				unitType = ("" === data.cssframework) ? "size" : "framework",
				options = null;

			//hide all value areas
			dialogContent.find('div.value-section').hide();
			dialogContent.find('div.value-section input:text').val("");
			dialogContent.find('div.value-section option').attr("selected", "").prop("selected", "");

			//toggle form visibility
			switch (unitType) {
				case "size":
					dialogContent.find('div#' + data.id + ' input:text').val(data.newwidth);
					break;
				case "framework":
					options = dialogContent.find('div#' + data.id + ' option');
					options.removeAttr('selected');
					options.filter(function (index) {
						var optionclasslist = this.value.split(" "),
                            selectedClasses = data.cssclass.split(" "),
                            i = 0;
						for (i = 0; i < optionclasslist.length; i = i + 1) {
							if ($.inArray(optionclasslist[i], selectedClasses) < 0) {
								return false;
							}
						}
						return true;
					}).attr('selected', true);
					break;
			}

			//set value section to be visible
			dialogContent.find('div#' + data.id).show();
		};
	}

	return ResizeViewModel;
});