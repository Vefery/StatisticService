import { Component } from '@angular/core';
import { of, Subscription, switchMap } from 'rxjs';
import { DeviceDTO } from 'src/app/DTOs/DeviceDTO';
import { StatisticEntryDTO } from 'src/app/DTOs/StatisticEntryDTO';
import { ApiHandlerService } from 'src/app/Services/api-handler.service';
import { StateService } from 'src/app/Services/state-service.service';

@Component({
  selector: 'app-details-page',
  templateUrl: './details-page.component.html',
  styleUrls: ['./details-page.component.less']
})
export class DetailsPageComponent {
  historyEntries: StatisticEntryDTO[] = [];
  currentDevice: DeviceDTO | undefined = undefined;
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
      .subscribe((info) => this.historyEntries = info);
  }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  onDeleteEntry(id: number) {
    this.http.deleteStatisticsEntry(id).subscribe({
        next: () => this.historyEntries = this.historyEntries.filter((entry) => entry.id !== id),
        error: (er) => console.error("Failed to delete entry", er)
    });
  }
  onDownload() {
    const jsonString = JSON.stringify(this.historyEntries.map(({ id, ...rest }) => rest));
    const blob = new Blob([jsonString], { type: 'application/json' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `${this.currentDevice!._id}_statistics.json`;
    a.click();
    window.URL.revokeObjectURL(url);
  }
}
