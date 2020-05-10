import {
  subtractTime,
  milliToFormat,
  ERROR,
} from "@/components/Timesheet/TimeFunctions.js";
var moment = require("moment");

let date_valid = "2000-01-01";

let subtract_null = [
  [null, null],
  [date_valid, null],
  [null, date_valid],
];

let subtract_malformat = [
  ["2020-12-2", "2020-12-20", "YYYY-MM-DD", ERROR],
  ["2020-12-20", "2020-12-2", "YYYY-MM-DD", ERROR],
  ["20s0-10-20", "2020-12-20", "YYYY-MM-DD", ERROR],
  ["2020-10-20", "20s0-12-20", "YYYY-MM-DD", ERROR],
  ["20:1", "20:20", "HH:MM", ERROR],
  ["20:s0", "20:20", "HH:MM", ERROR],
  ["20.20", "20:20", "HH:MM", ERROR],
  ["20:20", "20:1", "HH:MM", ERROR],
  ["20:20", "20:s0", "HH:MM", ERROR],
  ["20:20", "20:20", "HH:MM", ERROR],
  ["10:20 A", "10:20 AM", "HH:MM A", ERROR],
  ["10:s0 AM", "10:20 AM", "HH:MM A", ERROR],
  ["10:20", "10:20 AM", "HH:MM A", ERROR],
  ["13:20 AM", "10:20 AM", "HH:MM A", ERROR],
  ["00:20 AM", "10:20 AM", "HH:MM A", ERROR],
  ["10:20 am", "10:20 AM", "HH:MM A", ERROR],
  ["10:20 AM", "10:20 A", "HH:MM A", ERROR],
  ["10:20 AM", "10:s0 AM", "HH:MM A", ERROR],
  ["10:20 AM", "10:20", "HH:MM A", ERROR],
  ["10:20 AM", "13:20 AM", "HH:MM A", ERROR],
  ["00:20 AM", "00:20 AM", "HH:MM A", ERROR],
  ["10:20 AM", "10:20 am", "HH:MM A", ERROR],
];

describe("'subtractTime' function", () => {
  test.each(subtract_null)(
    "Given %p and %p as the first two arguments, return an error value",
    (firstArg, secondArg) => {
      expect(subtractTime(firstArg, secondArg, "")).toBe(ERROR);
    }
  );

  test.each(subtract_malformat)(
    "Given malformatted arguments %p and %p with expected format %p, return an error value",
    (firstArg, secondArg, thirdArg) => {
      expect(subtractTime(firstArg, secondArg, thirdArg)).toBe(ERROR);
    }
  );

  test.each`
    a                        | b                        | c                       | expectedFormat        | expectedValue
    ${"2020-12-20"}          | ${"2020-12-20"}          | ${"YYYY-MM-DD"}         | ${"YYYY-MM-DD"}       | ${"0000-00-00"}
    ${"2020-12-20"}          | ${"2020-12-20"}          | ${"YYYY-MM-DD"}         | ${"YYYY-MM-DD"}       | ${"0000-00-00"}
    ${"2020-12-20"}          | ${"2020-12-21"}          | ${"YYYY-MM-DD"}         | ${"YYYY-MM-DD"}       | ${"0000-00-01"}
    ${"2020-12-20 10:00 AM"} | ${"2020-12-20 10:00 AM"} | ${"YYYY-MM-DD HH:mm A"} | ${"YYYY-MM-DD HH:mm"} | ${"0000-00-00 00:00"}
    ${"2020-12-20 10:00 AM"} | ${"2020-12-20 10:30 AM"} | ${"YYYY-MM-DD HH:mm A"} | ${"YYYY-MM-DD HH:mm"} | ${"0000-00-00 00:30"}
    ${"2020-12-20 10:00 AM"} | ${"2020-12-20 10:00 PM"} | ${"YYYY-MM-DD HH:mm A"} | ${"YYYY-MM-DD HH:mm"} | ${"0000-00-00 12:00"}
    ${"2020-12-20 10:00 AM"} | ${"2020-12-20 10:30 PM"} | ${"YYYY-MM-DD HH:mm A"} | ${"YYYY-MM-DD HH:mm"} | ${"0000-00-00 12:30"}
    ${"10:00 AM"}            | ${"10:00 AM"}            | ${"HH:mm A"}            | ${"HH:mm"}            | ${"00:00"}
    ${"10:10 AM"}            | ${"10:45 AM"}            | ${"HH:mm A"}            | ${"HH:mm"}            | ${"00:35"}
    ${"10:10 AM"}            | ${"10:45 PM"}            | ${"HH:mm A"}            | ${"HH:mm"}            | ${"12:35"}
    ${"10:45 AM"}            | ${"10:10 AM"}            | ${"HH:mm A"}            | ${"HH:mm"}            | ${"-00:35"}
    ${"10:45 PM"}            | ${"10:10 AM"}            | ${"HH:mm A"}            | ${"HH:mm"}            | ${"-12:35"}
    ${"10:00"}               | ${"10:00"}               | ${"HH:mm"}              | ${"HH:mm"}            | ${"00:00"}
    ${"10:00"}               | ${"10:40"}               | ${"HH:mm"}              | ${"HH:mm"}            | ${"00:40"}
    ${"10:00"}               | ${"22:40"}               | ${"HH:mm"}              | ${"HH:mm"}            | ${"12:40"}
    ${"10:40"}               | ${"10:00"}               | ${"HH:mm"}              | ${"HH:mm"}            | ${"-00:40"}
    ${"22:40"}               | ${"10:00"}               | ${"HH:mm"}              | ${"HH:mm"}            | ${"-12:40"}
  `(
    "Given valid arguments $a and $b with expected format $c, return $expectedValue in format $expectedFormat",
    ({ a, b, c, expectedFormat, expectedValue }) => {
      var response = subtractTime(a, b, c);
      expect(milliToFormat(response, expectedFormat)).toBe(expectedValue);
    }
  );
});
