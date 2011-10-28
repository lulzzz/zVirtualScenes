Ext.require(['zvsMobile.store.Devices', 'Ext.dataview.List'], function () {

    Ext.define('zvsMobile.view.DeviceList', {
        extend: 'Ext.dataview.List',
        alias: 'widget.DeviceList',
        constructor: function (config) {
            var self = this;
            Ext.apply(config || {}, {
                scroll: 'vertical',
                items: [{
                    xtype: 'toolbar',
                    docked: 'top',
                    scrollable: false,
                    items: [{
                        xtype: 'button',
                        iconMask: true,
                        iconCls: 'refresh',
                        handler: function () {
                            DeviceStore.load();
                        }
                    }, {
                        xtype: 'spacer'
                    }, {
                        html: 'Devices',
                        style: 'color:#fff;font-weight:bold'
                    }, {
                        xtype: 'spacer'
                    }]
                }],

                listeners:
			{
			    scope: this,
			    selectionchange: function (list, records) {
			        if (records[0] !== undefined) {
			            var DeviceViewPort = self.parent;
			            var DimmmerDetails = DeviceViewPort.items.items[1];
			            var SwitchDetails = DeviceViewPort.items.items[2];
			            var TempDetails = DeviceViewPort.items.items[3];

			            if (records[0].data.type === 'DIMMER') {
			                DimmmerDetails.loadDevice(records[0].data.id);
			                DeviceViewPort.getLayout().setAnimation({ type: 'slide', direction: 'left' });
			                DeviceViewPort.setActiveItem(DimmmerDetails);
			            }

			            if (records[0].data.type === 'SWITCH') {
			                SwitchDetails.loadDevice(records[0].data.id);
			                DeviceViewPort.getLayout().setAnimation({ type: 'slide', direction: 'left' });
			                DeviceViewPort.setActiveItem(SwitchDetails);
			            }

			            if (records[0].data.type === 'THERMOSTAT') {
			                TempDetails.loadDevice(records[0].data.id);
			                DeviceViewPort.getLayout().setAnimation({ type: 'slide', direction: 'left' });
			                DeviceViewPort.setActiveItem(TempDetails);
			            }


			        }
			    },
			    activate: function () {
			        self.deselectAll();
			    }
			}
            });
            this.callOverridden([config]);
        },
        config:
    {
        itemTpl: new Ext.XTemplate(
		        '<div class="device">',
			        '<div class="imageholder {type}_{on_off}"></div>',
			        '<h2>{name}</h2>',
			        '<div class="level">',
				        '<div class="meter">',
					        '<div class="progress" style="width:{level}%">',
					        '</div>',
				        '</div>',
				        '<div class="percent">{level_txt}</div>',
			        '</div>',
		        '</div>'),
        cls: 'DeviceListItem',
        store: 'Devices'
    }
    });
});