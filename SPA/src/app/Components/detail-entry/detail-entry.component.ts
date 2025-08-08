import { Component, Input } from '@angular/core';
import { StatisticEntryDTO } from 'src/app/DTOs/StatisticEntryDTO';
import { StateService } from 'src/app/Services/state-service.service';

@Component({
  selector: 'app-detail-entry',
  templateUrl: './detail-entry.component.html',
  styleUrls: ['./detail-entry.component.less']
})
export class DetailEntryComponent {
  @Input() data!: StatisticEntryDTO; 
}
