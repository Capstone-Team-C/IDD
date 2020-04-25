let time_functions = {
  // Parses a time field like HH:mm AM/PM into an array of time sections
  parseTime(time) {
    var ret = [];

    // Check that time is long enough
    if (!time) return null;
    if (time.length < 5) return null;

    // Separate the HH:mm part
    ret.push(parseInt(time.substr(0, 2)));
    ret.push(parseInt(time.substr(3, 2)));

    // Separate the AM/PM part if it exists
    if (time.length > 5) {
      ret.push(time.substr(6, 2));
    }
    return ret;
  },

  // Compare two date fields like YYYY-mm-dd
  dateCompare(start, end) {
    if (!start || !end) return 1;

    // start is after end
    if (start > end) return 1;

    // start is before end
    if (start < end) return -1;

    // star and end are the same days
    return 0;
  },

  // Find the difference between two time fields like [HH, mm, AM/PM]
  subtractTime(a, b) {
    var start = a.slice();
    var end = b.slice();
    var ret = [0, 0];

    // Check that a and b are valid
    if (!start || !end) return 0;

    // If add 12 hours to fields with PM
    if (start.length === 3 && start[2] === "PM") start[0] += 12;
    if (end.length === 3 && end[2] === "PM") end[0] += 12;

    // Calculate the time difference
    ret[0] = end[0] - start[0];

    if (ret[0] > 0) {
      // start is earlier than end; start->end minutes
      ret[1] = 60 - start[1] + end[1];
      if (ret[1] < 60) {
        ret[0] -= 1;
      } else if (ret[1] > 60) {
        ret[0] += 1;
        ret[1] -= 60;
      } else ret[1] = 0;
    } else if (ret[0] < 0) {
      // start is later than end; end->start minutes
      ret[1] = 60 - end[1] + start[1];
      if (ret[1] < 60) {
        ret[0] -= 1;
      } else if (ret[1] > 60) {
        ret[0] += 1;
        ret[1] -= 60;
      } else ret[1] = 0;
      ret[1] *= -1;
    } else {
      // start and end are in the same hour
      ret[1] = end[1] - start[1];
    }
    return ret;
  },
};

export default time_functions;
