import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-list-entry',
  templateUrl: './list-entry.component.html',
  styleUrls: ['./list-entry.component.less']
})
export class ListEntryComponent {
  @Input() deviceId!: string;
  @Input() name!: string;

  @Output() entryClick = new EventEmitter();

  OnClick() {
    this.entryClick.emit()
  }
}
