import { Injectable } from '@angular/core';
import { StatisticEntryDTO } from '../DTOs/StatisticEntryDTO';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { DeviceDTO } from '../DTOs/DeviceDTO';

@Injectable({
  providedIn: 'root'
})
export class ApiHandlerService {
    private apiUrl = 'https://localhost:44362';

    constructor(private http: HttpClient) { }

    getAllDevices(): Observable<DeviceDTO[]> {
      try {
        return this.http.get<DeviceDTO[]>(`${this.apiUrl}/statistics/devices`);
      } catch (e) {
        console.error('Ошибка получения данных с сервера: ', e)
        return of([]);
      }
    }

    getDeviceInfo(id: string): Observable<StatisticEntryDTO[]> {
      try {
        return this.http.get<StatisticEntryDTO[]>(`${this.apiUrl}/statistics/devices/${id}`);
      } catch (e) {
        console.error('Ошибка получения данных с сервера: ', e)
        return of([]);
      }
    }
}
