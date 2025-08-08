import { Injectable } from '@angular/core';
import { StatisticEntryDTO } from '../DTOs/StatisticEntryDTO';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { DeviceDTO } from '../DTOs/DeviceDTO';

@Injectable({
  providedIn: 'root'
})
export class ApiHandlerService {
    private apiUrl = 'https://your-api-endpoint.com';

    constructor(private http: HttpClient) { }

    async getAllDevices(): Promise<DeviceDTO[] | undefined> {
      try {
        const request$ = this.http.get<DeviceDTO[]>(`${this.apiUrl}/statistics/devices`);
        return await lastValueFrom(request$);
      } catch (e) {
        console.error('Ошибка получения данных с сервера: ', e)
        return undefined;
      }
    }

    async getDeviceInfo(id: string): Promise<StatisticEntryDTO[] | undefined> {
      try {
        const request$ = this.http.get<StatisticEntryDTO[]>(`${this.apiUrl}/statistics/devices/${id}`);
        return await lastValueFrom(request$);
      } catch (e) {
        console.error('Ошибка получения данных с сервера: ', e)
        return undefined;
      }
    }
}
