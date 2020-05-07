import { mount, shallowMount } from '@vue/test-utils'
import sinon from 'sinon'
import time_functions from "@/components/Timesheet/TimeFunctions.js";

let a = null;
let b = null;

test('Invalid dates should return a 0', () => {
  expect(time_functions.dateCompare(a, b)).toBe(1)
})


