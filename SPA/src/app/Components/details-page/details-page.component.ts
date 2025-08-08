import { Component } from '@angular/core';
import { catchError, of, Subscription, switchMap } from 'rxjs';
import { GeneralDeviceDTO, StatisticEntryDTO } from 'src/app/DTOs/StatisticEntryDTO';
import { ApiHandlerService } from 'src/app/Services/api-handler.service';
import { StateService } from 'src/app/Services/state-service.service';

@Component({
  selector: 'app-details-page',
  templateUrl: './details-page.component.html',
  styleUrls: ['./details-page.component.less']
})
export class DetailsPageComponent {
  historyEntries: StatisticEntryDTO[] = [];
  currentDevice: GeneralDeviceDTO | undefined = undefined;
  private subscription: Subscription = new Subscription;

  constructor(private state: StateService, private http: ApiHandlerService) {}

  ngOnInit(): void {
    this.subscription = this.state.currentDevice$.pipe(
      switchMap(
        device => {
          if (device === undefined)
            return of([])
          else {
            this.currentDevice = device;
            return this.http.getDeviceInfo(device._id);
          }
        }
      )
    )
      .subscribe((info) => {console.log(info); this.historyEntries = info});
  }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
