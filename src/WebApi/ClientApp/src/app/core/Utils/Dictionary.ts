/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
/* eslint-disable @typescript-eslint/no-explicit-any */

interface IDictionary<T> {
  add(key: string, value: T): void;
  remove(key: string): void;
  containsKey(key: string): boolean;
  keys(): string[];
  values(): T[];
  length: number;
}

export class Dictionary<T> implements IDictionary<T> {
  _keys: string[] = [];
  _values: T[] = [];

  constructor();
  constructor(init?: IDictionary<T>) {
    if (init !== undefined) {
      for (let x = 0; x < init.length; x++) {
        this[init[x].key] = init[x].value;
        this._keys.push(init[x].key);
        this._values.push(init[x].value);
      }
    }
  }

  public add(key: string, value: T): void {
    this[key] = value;
    this._keys.push(key);
    this._values.push(value);
  }

  public remove(key: string): void {
    const index = this._keys.indexOf(key, 0);
    this._keys.splice(index, 1);
    this._values.splice(index, 1);

    delete this[key];
  }

  public keys(): string[] {
    return this._keys;
  }

  public values(): T[] {
    return this._values;
  }

  public containsKey(key: string): boolean {
    if (typeof this[key] === 'undefined') {
      return false;
    }
    return true;
  }

  public get length(): number {
    return this._keys.length;
  }

  public toLookup(): IDictionary<T> {
    return this;
  }
}
