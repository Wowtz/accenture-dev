import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[appCpfCnpjFormat]'
})
export class CpfCnpjFormatDirective {
  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event'])
  onInput(event: Event) {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); 

    if (value.length <= 11) {
      if (value.length > 3) {
        value = value.substring(0, 3) + '.' + value.substring(3);
      }
      if (value.length > 7) {
        value = value.substring(0, 7) + '.' + value.substring(7);
      }
      if (value.length > 11) {
        value = value.substring(0, 11) + '-' + value.substring(11);
      }
    } else {
      if (value.length > 2) {
        value = value.substring(0, 2) + '.' + value.substring(2);
      }
      if (value.length > 6) {
        value = value.substring(0, 6) + '.' + value.substring(6);
      }
      if (value.length > 10) {
        value = value.substring(0, 10) + '/' + value.substring(10);
      }
      if (value.length > 15) {
        value = value.substring(0, 15) + '-' + value.substring(15);
      }
    }

    input.value = value;
  }
}
