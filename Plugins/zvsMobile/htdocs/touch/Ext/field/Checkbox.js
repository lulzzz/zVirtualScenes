/**
The checkbox field is an enhanced version of the native browser checkbox and is great for enabling your user to
choose one or more items from a set (for example choosing toppings for a pizza order). It works like any other
{@link Ext.field.Field field} and is usually found in the context of a form:
    
## Example

    @example preview
    var form = Ext.create('Ext.form.Panel', {
        fullscreen: true,
        items: [
            {
                xtype: 'checkboxfield',
                name : 'tomato',
                label: 'Tomato',
                value: 'tomato',
                checked: true
            },
            {
                xtype: 'checkboxfield',
                name : 'salami',
                label: 'Salami'
            },
            {
                xtype: 'toolbar',
                docked: 'bottom',
                items: [
                    { xtype: 'spacer' },
                    {
                        text: 'getValues',
                        handler: function() {
                            var form = Ext.ComponentQuery.query('formpanel')[0],
                                values = form.getValues();
                            
                            Ext.Msg.alert(null, 
                                "Tomato: " + ((values.tomato) ? "yes" : "no")
                                + "<br />Salami: " + ((values.salami) ? "yes" : "no")
                            );
                        }
                    },
                    { xtype: 'spacer' }
                ]
            }
        ]
    });


The form above contains two check boxes - one for Tomato, one for Salami. We configured the Tomato checkbox to be
checked immediately on load, and the Salami checkbox to be unchecked. We also specified an optional text
{@link #value} that will be sent when we submit the form. We can get this value using the Form's
{@link Ext.form.Panel#getValues getValues} function, or have it sent as part of the data that is sent when the
form is submitted:

    form.getValues(); //contains a key called 'tomato' if the Tomato field is still checked
    form.submit(); //will send 'tomato' in the form submission data

 */
Ext.define('Ext.field.Checkbox', {
    extend: 'Ext.field.Field',
    alternateClassName: 'Ext.form.Checkbox',

    xtype: 'checkboxfield',

    /**
     * @event check
     * Fires when the checkbox is checked.
     * @param {Ext.field.Checkbox} this This checkbox
     */

    /**
     * @event uncheck
     * Fires when the checkbox is unchecked.
     * @param {Ext.field.Checkbox} this This checkbox
     */
    config: {
        // @inherit
        ui: 'checkbox',

        /**
         * @cfg {String} value The string value to submit if the item is in a checked state.
         * @accessor
         */
        value: '',

        /**
         * @cfg {Boolean} checked <tt>true</tt> if the checkbox should render initially checked
         * @accessor
         */
        checked: false,

        /**
         * @cfg {Number} tabIndex
         * @hide
         * @accessor
         */
        tabIndex: -1,

        // @inherit
        input: {
            type    : 'checkbox',
            inputCls: Ext.baseCSSPrefix + 'input-checkbox'
        }
    },

    // @private
    initialize: function() {
        var me = this;

        me.callParent(arguments);

        me.on({
            scope   : me,
            delegate: 'input',

            click: 'onClick',
            masktap: 'onMaskTap'
        });
    },

    // @private
    doInitValue: function() {
        var me = this;

        me.originalState = me.getInitialConfig().checked;

        me.callParent(arguments);
    },

    /**
     * Returns the field checked value
     * @return {Mixed} The field value
     */
    getChecked: function() {
        var input = this.getInput();

        // we need to get the latest value from the {@link #input} and then update the value
        var checked = input.getChecked();
        this._checked = checked;

        return this._checked;
    },

    getValue: function() {
        return this.getChecked();
    },

    setChecked: function(newChecked) {
        this.getChecked(); //we do this to sync the input field and field values
        this.updateChecked(newChecked);
        this._checked = newChecked;
    },

    updateChecked: function(newChecked) {
        var input = this.getInput();
        input.setChecked(newChecked);
    },

    // @private
    onMaskTap: function(input, e) {
        if (this.getDisabled()) {
            return false;
        }

        //we must manually update the input dom with the new checked value
        input.input.dom.checked = !input.input.dom.checked;

        //continue as normal, like a normal tap
        this.onTap(input, e);

        //return false so the mask does not disappear
        return false;
    },

    // @private
    onClick: function(input, e) {
        var me = this;

        //calling getchecked will sync the new checked value
        if (me.getChecked()) {
            me.fireAction('uncheck', [me, e], 'doUnChecked');
        } else {
            me.fireAction('check', [me, e], 'doChecked');
        }
    },

    /**
     * @method
     * Method called when this {@link Ext.field.Checkbox} has been checked
     */
    doChecked: Ext.emptyFn,

    /**
     * @method
     * Method called when this {@link Ext.field.Checkbox} has been unchecked
     */
    doUnChecked: Ext.emptyFn,

    /**
     * Returns the checked state of the checkbox.
     * @return {Boolean} True if checked, else otherwise
     */
    isChecked: function() {
        return this.getChecked();
    },

    /**
     * Set the checked state of the checkbox to true
     * @return {Ext.field.Checkbox} This checkbox
     */
    check: function() {
        return this.setChecked(true);
    },

    /**
     * Set the checked state of the checkbox to false
     * @return {Ext.field.Checkbox} This checkbox
     */
    uncheck: function() {
        return this.setChecked(false);
    },

    getSameGroupFields: function() {
        var component = this.up('formpanel') || this.up('fieldset');

        if (!component) {
            return null;
        }

        return component.query('[name=' + this.getName() + ']');
    },

    /**
     * Returns an array of values from the checkboxes in the group that are checked,
     * @return {Array}
     */
    getGroupValues: function() {
        var values = [];

        this.getSameGroupFields().forEach(function(field) {
            if (field.getChecked()) {
                values.push(field.getValue());
            }
        });

        return values;
    },

    /**
     * Set the status of all matched checkboxes in the same group to checked
     * @param {Array} values An array of values
     * @return {Ext.field.Checkbox} This checkbox
     */
    setGroupValues: function(values) {
        this.getSameGroupFields().forEach(function(field) {
            field.setChecked((values.indexOf(field.getValue()) !== -1));
        });

        return this;
    },

    /**
     * Resets the status of all matched checkboxes in the same group to checked
     * @param {Array} values An array of values
     * @return {Ext.field.Checkbox} This checkbox
     */
    resetGroupValues: function() {
        this.getSameGroupFields().forEach(function(field) {
            field.setChecked(field.originalState);
        });

        return this;
    },

    // @inherit
    reset: function() {
        this.callParent(arguments);
        this.resetGroupValues();
    }
});
