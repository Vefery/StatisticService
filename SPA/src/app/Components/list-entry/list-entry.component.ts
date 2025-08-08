import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DeviceDTO } from 'src/app/DTOs/DeviceDTO';

@Component({
  selector: 'app-list-entry',
  templateUrl: './list-entry.component.html',
  styleUrls: ['./list-entry.component.less']
})
export class ListEntryComponent {
  @Input() data!: DeviceDTO;

  @Output() entryClick = new EventEmitter<DeviceDTO>();

  OnClick(device: DeviceDTO) {
    this.entryClick.emit(device);
  }
}