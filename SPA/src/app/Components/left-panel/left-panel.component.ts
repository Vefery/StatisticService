import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { DeviceDTO } from 'src/app/DTOs/DeviceDTO';
import { ApiHandlerService } from 'src/app/Services/api-handler.service';
import { StateService } from 'src/app/Services/state-service.service';

@Component({
  selector: 'app-left-panel',
  templateUrl: './left-panel.component.html',
  styleUrls: ['./left-panel.component.less']
})
export class LeftPanelComponent implements OnInit {
  devices: DeviceDTO[] = [];
  private subscription: Subscription = new Subscription;

  constructor (private apiHandler: ApiHandlerService, private state: StateService) {}

  ngOnInit(): void {
    this.subscription = this.apiHandler.getAllDevices().subscribe((devices) => this.devices = devices);
    this.state.updateDevicesEvent.subscribe(() => {
      this.subscription = this.apiHandler.getAllDevices().subscribe(
        (devices) => this.devices = devices
      );
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onSelect(device: DeviceDTO) {
    this.state.setCurrentDevice(device);
  }
}
