import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StatisticEntryDTO } from 'src/app/DTOs/StatisticEntryDTO';

@Component({
  selector: 'app-detail-entry',
  templateUrl: './detail-entry.component.html',
  styleUrls: ['./detail-entry.component.less']
})
export class DetailEntryComponent {
  @Input() data!: StatisticEntryDTO;
  @Output() deleteClick = new EventEmitter<number>();

  onDelete(id: number) {
    this.deleteClick.emit(id);
  }
}
