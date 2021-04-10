import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/Constants';

@Pipe({
  name: 'DateTimeFormatter'
})
export class DateTimeFormatterPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    value = Date.parse(value);
    return super.transform(value, Constants.DATETIME_FMT);
  }

}
