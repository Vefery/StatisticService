export interface GeneralDeviceDTO {
    _id: string;
    name: string;
}

export interface StatisticEntryDTO extends GeneralDeviceDTO {
    id: number;
    startTime: Date;
    endTime: Date;
    version: string;
}