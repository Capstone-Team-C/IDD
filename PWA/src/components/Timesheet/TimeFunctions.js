var moment = require('moment');
var momentDurationFormatSetup = require("moment-duration-format");
momentDurationFormatSetup(moment);

export const FULL_DATE = "YYYY-MM-DD HH:mm A";
export const YEAR_MONTH_DAY = "YYYY-MM-DD";
export const YEAR_MONTH = "YYYY-MM";
export const TIME_12 = "HH:mm A";
export const TIME_24 = "HH:mm";
export const ERROR = -0;

/**
 *
 * description
 * Paramters
 *      paratmer: <type> - description of parameter
 * Parameter 1
 * Parameter 2
 * Returns
 *      return value
 *
 * 
 *
 * */
// Compare two date fields like YYYY-MM-DD
// Find the difference in milliseconds between two time fields
export const subtractTime = (start, end, format) => {
  if (start == undefined || end == undefined || format == undefined) return ERROR; 
  
  const formattedStart = moment(start, format, true);
  const formattedEnd = moment(end, format, true);
  
  if (!formattedStart.isValid() || !formattedEnd.isValid()){
    return ERROR;
  }
  
  const difference = moment.duration(formattedEnd.diff(formattedStart));
  if (difference === 0) {
    return 0;
  }

  return difference.asMilliseconds();
}



// Convert milliseconds to a certain format
export const milliToFormat = (milli, format) => {
  if (milli == undefined) return ERROR;
  let ret = "";  
  if (milli < 0) ret += "-";
  
  const dur = moment.duration(Math.abs(milli));
  ret += dur.format(format, { trim: false, useGrouping: false }); 
  
  return ret;
}
