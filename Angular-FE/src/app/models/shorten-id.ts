    import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'shortenId',
  standalone: true
})
export class ShortenIdPipe implements PipeTransform {
  transform(value: string, visibleChars: number = 6): string {
    if (!value || value.length <= visibleChars * 2) return value;
    return `${value.slice(0, visibleChars)}...${value.slice(-visibleChars)}`;
  }
}
