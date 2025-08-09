import { DeviceDTO } from "./DeviceDTO";

export interface StatisticEntryDTO extends DeviceDTO {
    id: number;
    startTime: Date;
    endTime: Date;
    version: string;
}