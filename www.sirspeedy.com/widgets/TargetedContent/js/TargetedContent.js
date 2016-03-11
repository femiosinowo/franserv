
if ("undefined" == typeof Ektron.Widget) Ektron.Widget = {};
if ("undefined" == typeof Ektron.Widget.TargetedContent) Ektron.Widget.TargetedContent =
{
    isUX: false,
    instances: [],
    init: function (id, zoneCount) {
        //is awesomizer enabled
        this.isUX = $ektron("[data-ektron-pagebuilder-page]").length > 0 ? true : false;

        var objInstance = Ektron.Widget.TargetedContent.instances[id],
            setSelectedCondition = function (settings) {
                var s = {
                    context: 'init', // 'selectColumn.uxSiteApp'
                    sourceData: null,
                    event: null,
                    index: null
                };

                s = $ektron.extend(s, settings);

                objInstance.$wrapper.find('[data-ux-pagebuilder-column-data]').each(function (i) {
                    var $this = $(this),
                        selectedColumnClass = 'targetContentColumnSelected',
                        data = $this.data('uxPagebuilderColumnData'),
                        ul = $this.find('> ul'),
                        setSelected = function () {
                            objInstance.$wrapper.find('[data-ux-pagebuilder-column-data] > ul').removeClass(selectedColumnClass);
                            objInstance.$wrapper.find('input:first').val(i);
                            ul.addClass(selectedColumnClass);
                        };

                    switch (s.context) {
                        case 'init':

                            if (i === s.index) {
                                setSelected();
                            }
                            break;
                        case 'selectColumn.uxSiteApp':
                            if (s.sourceData.columnData && s.sourceData.columnData.ColumnGUID == data.ColumnGUID) {
                                setSelected();
                            }
                            break;
                        default:
                            // do nothing
                            break;
                    }
                });
            };
        
        $ektron(window).on('selectColumn.uxSiteApp', function (ev, sourceData) {
            setSelectedCondition({
                context: 'selectColumn.uxSiteApp',
                sourceData: sourceData,
                event: ev
            });
        });

        if (!objInstance) {
            objInstance = new Ektron.Widget.TargetedContent.Instance();
            Ektron.Widget.TargetedContent.instances[id] = objInstance;
        }

        objInstance.selectColumn = function (index) {
            setSelectedCondition({
                context: 'init',
                index: index
            });
        };

        objInstance.$wrapper = $ektron("#" + id);
        objInstance.$selectedZone = objInstance.$wrapper.find("> input[id*='SelectedZone']");
        objInstance.$myhidden = objInstance.$wrapper.find("> input[id*='HiddenField1']");
        objInstance.$columnsContainer = objInstance.$wrapper.find("> div.columns-container");

        if (this.isUX) {
            objInstance.$conditionalZones = objInstance.$columnsContainer.find("> div[data-ux-pagebuilder='Column']");
        } else {
            objInstance.$conditionalZones = objInstance.$columnsContainer.find("> div.PBColumn");
        }

        if (this.isUX) {
            objInstance.$conditionalZoneLists = objInstance.$conditionalZones.find("> div[data-ux-pagebuilder='Column'] > ul");
        } else {
            objInstance.$conditionalZoneLists = objInstance.$conditionalZones.find("> ul.columnwidgetlist");
        }

        objInstance.zoneCount = zoneCount;
        objInstance.selectedZone = $ektron.toInt(objInstance.$selectedZone.val());

        objInstance.hideAllZones();

        //only initialize sortables if Awesomizer is not enabled
        if (!this.isUX) {
            if (objInstance.$columnsContainer.is("ui-sortable")) {
                objInstance.$columnsContainer.sortable("destroy");
            }

            objInstance.$columnsContainer.sortable(
                {
                    items: "div.PBColumn",
                    handle: "> ul.columnwidgetlist > li.header",
                    containment: "parent",
                    forceHelperSize: true,
                    axis: "y",
                    cursor: "move",
                    delay: 200,
                    cancel: ".selectedZone",
                    stop: function (e, ui) {
                        var index = $ektron(ui.item).closest("div.PBColumn").index();
                        objInstance.$selectedZone.val(index);

                        var strZoneOrder = objInstance.$columnsContainer.sortable("serialize", { key: "zone", attribute: "id", expression: /(\d+)_zone/ });
                        // format is "zone=nn&zone=nn&zone=nn" where nn is the 0-based index
                        strZoneOrder = (new Ektron.String(strZoneOrder)).trim().replace("zone=", "").replace("&", ",").toString();
                        objInstance.$wrapper.find("> input[id*='ZoneOrder']").val(strZoneOrder);
                        objInstance.$wrapper.find(".targetedContent-changeZoneOrder").click();
                        // click of changeZoneOrder performs a postback
                    }
                });
        } else {
            if (this.isUX) {
                $ektron("select.ux-condition-change").each(function () {
                    var select = $ektron(this),
                        selectedIndex = select.val();
                    select.attr("data-ektron-targetedContent-original-index", selectedIndex);
                });
                $ektron("select.ux-condition-change").on("change", function () {
                    var changedSelect = $ektron(this),
                        changedSelectOriginalSelection = parseInt(changedSelect.attr("data-ektron-targetedContent-original-index"), 10),
                        changedSelectCurrentSelection = parseInt(changedSelect.val(), 10),
                        order = [],
                        siblings = changedSelect.closest(".targetedContent").find("select.ux-condition-change");


                    var originalSelection,
                        currentSelection;
                    siblings.each(function (i) {
                        var select = $ektron(this);
                            
                        order.push(i);
                        if (select.attr("id") === changedSelect.attr("id")) {
                            originalSelection = (parseInt(select.attr("data-ektron-targetedContent-original-index"), 10) - 1),
                            currentSelection = (parseInt(select.val(), 10) - 1);
                        }
                    });

                    order.move(originalSelection, currentSelection);
                    changedSelect.closest(".targetedContent").find("> input[id*='ZoneOrder']").val(order.join(','));
                    changedSelect.closest(".targetedContent").find(".targetedContent-changeZoneOrder").click();
                });
            }
        }

        if (this.isUX) {
            objInstance.$conditionalZoneLists.find("> div.columnHeader").click(function () {
                var index = $ektron(this).closest("div[data-ux-pagebuilder='Column']").index();
                objInstance.showConditionalZone(index);
            });
        } else {
            objInstance.$conditionalZoneLists.find("> li.header").click(function () {
                var index = $ektron(this).closest("div.PBColumn").index();
                objInstance.showConditionalZone(index);
            });
        }

        objInstance.showConditionalZone(objInstance.selectedZone);

        $ektron("div.targetContentListModal").dialog({
            autoOpen: false,
            resizable: true,
            create: function (event, ui) {
                $ektron("div.targetContentListModal").closest(".ui-dialog").css("z-index", "9999");
            }
        });
        $ektron("div.targetContentListModal").parent().appendTo($("form:first"));

        return objInstance;
    },

    Instance: function () {
        this.zoneCount = 0;
        this.selectedZone = 0;
        this.showConditionalZone = Ektron.Widget.TargetedContent.p_Instance_showConditionalZone;
        this.hideAllZones = Ektron.Widget.TargetedContent.p_Instance_hideAllZones;
    },

    p_Instance_showConditionalZone: function (index) {
        if (Ektron.Widget.TargetedContent.isUX) {
            this.selectColumn(index);
        }

        var $conditionalZoneList = $ektron(this.$conditionalZoneLists[this.selectedZone]);

        var $cookievalue = "";
        if ($ektron.cookie("targetcontentcookie")) {
            $cookievalue = $ektron.cookie("targetcontentcookie");
        }
        var arrayList = $cookievalue.split(',');
        var $totalWidget = "";
        var $k = 0;
        var $currentColumn = index;

        var columnSelector = "> div.PBColumn";
        var byColumnListSelector = "> ul.columnwidgetlist > li.PBItem";
        var me = Ektron.Widget.TargetedContent;

        this.$columnsContainer.find(columnSelector).each(function () {
            var $byColumnList = $(this).find(byColumnListSelector).length;
            var $pair = $k + "|" + $byColumnList;
            $totalWidget += $pair + ",";
            for (var $kk = 0; $kk < arrayList.length - 1; $kk++) {
                if (arrayList[$kk] != $pair && arrayList[$kk] != '' && $k == $kk) {
                    $currentColumn = $k;
                }
            }

            $k++;
        });

        $ektron.cookie("targetcontentcookie", $totalWidget);

        var headerSelector = "> li.header";
        var itemSelector = "> li.PBItem";
        
        $conditionalZoneList.find(headerSelector).removeClass("selectedZone");
        $conditionalZoneList.find(itemSelector).hide();
        $conditionalZoneList.css("min-height", 0);

        this.selectedZone = $currentColumn;                                     
        this.$selectedZone.val($currentColumn);                            
        $conditionalZoneList = $ektron(this.$conditionalZoneLists[$currentColumn]);

        $conditionalZoneList.css("min-height", '200px');
        $conditionalZoneList.find(itemSelector).show();
        $conditionalZoneList.find(headerSelector).addClass("selectedZone");
    },

    p_Instance_hideAllZones: function () {
        var headerSelector = "> li.header";
        var itemSelector = "> li.PBItem";
        if (this.isUx) {
            headerSelector = "> li.header";
            itemSelector = "> li";
        }

        this.$conditionalZoneLists.find(headerSelector).removeClass("selectedZone");
        this.$conditionalZoneLists.find(itemSelector).hide();
        this.$conditionalZoneLists.css("min-height", 0);
    }
};

if ("undefined" == typeof Ektron.Widget.TargetedContentList) Ektron.Widget.TargetedContentList =
{
    init: function() {
        $ektron(document).bind("Ektron.Controls.ClientPaging.PageEvent", null, function(ev, data) {
            var url = $ektron("input.hdnTargetContentListUrl").val() + " div.targetContentListModal table.ektronGrid tbody";
            $ektron("div.targetContentListModal table.ektronGrid").load(url, data);
        });
    },
    showDialog: function(selectSourceControl) {
		$('.delete').click(function() 
		{
			$ektron("div.targetContentListModal:first").dialog('close');
		});
        var dialog = $ektron("div.targetContentListModal");
        dialog.parent().appendTo($("form:first"));

        $ektron("input.hdnTargetContentControlSource").val(selectSourceControl);
        $ektron("div.targetContentListModal:first").dialog('open');

    },
    select: function(id) {
        $ektron("input.hdnTargetContentSelectId").val(id);
        return false;
    }
};

Ektron.ready(function() {
    Ektron.Widget.TargetedContentList.init();
});


Array.prototype.move = function (from, to) {
  this.splice(to, 0, this.splice(from, 1)[0]);
};