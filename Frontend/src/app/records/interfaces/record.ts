import {ITrack} from "./track";

export interface IRecord {
  id: string,
  bandId: string,
  bandName: string,
  title: string,
  mediaTypeId: string,
  releaseYear?: number,
  tracks: ITrack[],
}
