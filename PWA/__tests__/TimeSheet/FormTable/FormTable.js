import Vue from "vue";
import Vuetify from "vuetify";
import FormTable from "@/components/Timesheet/FormTable.vue";
import valid_timesheet from "./valid_timesheet.json";
import conflicting_timesheet from "./conflicting_timesheet.json";

import { mount, createLocalVue } from "@vue/test-utils";

Vue.use(Vuetify);

// describe has the name of the file
// it has the name of the functionality I am testingj
describe("FormTable", () => {
  let valid_props = {};
  valid_props["parsed_value"] = valid_timesheet["timesheet"];
  valid_props["value"] = valid_timesheet["timesheet"];
  valid_props["disabled"] = true;

  // Given no values, the table should still load
  it("Given empty props, the table should load with no entries or errors", () => {
    const localVue = createLocalVue();
    const wrapper = mount(FormTable, {
      localVue,
      vuetify: new Vuetify(),
    });
    expect(wrapper.vm.allEntries.length).toBe(0);
    expect(wrapper.vm.amtEdited).toBe(0);
    expect(wrapper.vm.validate()).toBe(0);
  });

  // Given a valid timesheet table .json, the user should not get an error.
  it("Given valid props, the table should load with no errors", () => {
    const localVue = createLocalVue();
    const wrapper = mount(FormTable, {
      localVue,
      vuetify: new Vuetify(),
      propsData: valid_props,
    });
    expect(wrapper.vm.allEntries.length).toBe(valid_props["value"].length);
    expect(wrapper.vm.amtEdited).toBe(0);
    var amtErrors = wrapper.vm.validate();
    wrapper.vm.printErrors();
    expect(amtErrors).toBe(0);
  });

  // Invalid timesheet entries should have an error
  it("Given mal-formatted props, the table should load with errors", () => {
    const localVue = createLocalVue();
    const wrapper = mount(FormTable, {
      localVue,
      vuetify: new Vuetify(),
      propsData: {
        parsed_value: [
          { date: "s019-10-05" },
          { starttime: "09:00AM" },
          { endtime: "1000 PM" },
          { totalHours: "11.00" },
        ],
      },
    });
    expect(wrapper.vm.allEntries.length).toBe(4);
    expect(wrapper.vm.amtEdited).toBe(0);

    var amtErrors = wrapper.vm.validate();
    const expectedAmtErrors = 16; // 4 invalid rows w/ 4 fields each
    if (amtErrors !== expectedAmtErrors) {
      wrapper.vm.printErrors();
    }
    expect(amtErrors).toBe(expectedAmtErrors);
  });

  let conflicting_props = {};
  conflicting_props["parsed_value"] = conflicting_timesheet["timesheet"];
  conflicting_props["value"] = conflicting_timesheet["timesheet"];
  conflicting_props["disabled"] = true;

  // Valid entries that conflict should return an error
  it("Given invalid props, the table should load with errors", () => {
    const localVue = createLocalVue();
    const wrapper = mount(FormTable, {
      localVue,
      vuetify: new Vuetify(),
      propsData: conflicting_props,
    });
    expect(wrapper.vm.allEntries.length).toBe(2);
    expect(wrapper.vm.amtEdited).toBe(0);

    var amtErrors = wrapper.vm.validate();
    const expectedAmtErrors = 2; // 2 valid rows, but conflicting time
    if (amtErrors !== expectedAmtErrors) {
      wrapper.vm.printErrors();
    }
    expect(amtErrors).toBe(expectedAmtErrors);
    expect(wrapper.vm.checkOverlapping()).toBe(1);
  });
});
