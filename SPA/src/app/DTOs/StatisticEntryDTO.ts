export interface GeneralDeviceDTO {
    _id: string;
    name: string;
}

export interface StatisticEntryDTO extends GeneralDeviceDTO {
    startTime: Date;
    endTime: Date;
    version: string;
}