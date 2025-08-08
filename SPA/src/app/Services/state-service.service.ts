import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { DeviceDTO } from '../DTOs/DeviceDTO';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  private currentDeviceIdSubject = new BehaviorSubject<DeviceDTO | undefined>(undefined);
  currentDevice$: Observable<DeviceDTO | undefined> = this.currentDeviceIdSubject.asObservable();

  constructor() { }

  setCurrentDevice(device: DeviceDTO) {
    this.currentDeviceIdSubject.next(device);
  }
}
