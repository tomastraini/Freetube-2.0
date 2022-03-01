import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tableFilter'
})
export class TableFilterPipe implements PipeTransform {

  transform(li: any[], value: string): any {
   return value !== undefined && li !== undefined ? li.filter(li =>
        li.title.indexOf(value) !== -1 ||

        li.description.indexOf(value) !== -1 ||

        li.title.toLowerCase().indexOf(value.toLowerCase()) !== -1 ||

        li.description.toLowerCase().indexOf(value.toLowerCase()) !== -1
    ) : li;
  }
}
