import { Injectable } from '@angular/core';
import { StatisticEntryDTO } from '../DTOs/StatisticEntryDTO';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, of } from 'rxjs';
import { DeviceDTO } from '../DTOs/DeviceDTO';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiHandlerService {
    private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getAllDevices(): Observable<DeviceDTO[]> {
      return this.http.get<DeviceDTO[]>(`${this.apiUrl}/devices`).pipe(
        catchError((err) => {
          console.error('Ошибка получения данных с сервера: ', err)
          return of([]);
        })
      );
    }

    deleteStatisticsEntry(id: number): Observable<void> {
      return this.http.delete<void>(`${this.apiUrl}/devices/id/statistics/${id}`).pipe(
        catchError((err) => {
          console.error('Ошибка при удалении: ', err)
          return of();
        })
      );
    }

    getDeviceInfo(id: string): Observable<StatisticEntryDTO[]> {
      return this.http.get<StatisticEntryDTO[]>(`${this.apiUrl}/devices/${id}/statistics`).pipe(
        map(entries => entries.map(
          entry => ({
            ...entry,
            startTime: new Date(entry.startTime),
            endTime: new Date(entry.endTime)
          })
        )),
        catchError((err) => {
          console.error('Ошибка получения данных с сервера: ', err)
          return of([]);
        })
      );
    }
}
