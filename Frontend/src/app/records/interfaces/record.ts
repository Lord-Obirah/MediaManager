import {ITrack} from "./track";

export interface IRecord {
  id: string,
  title: string,
  mediaTypeId: string,
  releaseYear?: number,
  tracks: ITrack[],
}
