import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[appCepFormat]'
})
export class CepFormatDirective {
  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event'])
  onInput(event: Event) {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); // Remove todos os caracteres não numéricos

    // Formata o CEP adicionando hífens
    if (value.length > 5) {
      value = value.substring(0, 5) + '-' + value.substring(5);
    }

    // Limita o CEP a 8 dígitos
    if (value.length > 9) {
      value = value.substring(0, 8);
    }

    input.value = value;
  }
}
