/**
 * Contains the data of one single select item
 */
export class AppSingleSelectData {
  /**
   * A unique identifier used for the internal identification of the item in the single select list.
   */
  public dxGuid: number;

  /**
   * The data column which value should be displayed to the user for the item.
   */
  public displayExpr: string;

  /**
   * The data column which provides a unique value for the item.
   */
  public valueExpr: any;

  /**
   * An icon specified for the item.
   */
  public icon?: string;

  /**
   * A color specified for the item.
   */
  public color?: string;

  /**
   * The object data of the item. Can be an object or a primitive data type.
   */
  public object?: any;
}
