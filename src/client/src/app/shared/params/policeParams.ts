export class FilterItem {
  field: string;
  value: string;

  constructor() {
    this.field = '';
    this.value = '';
  }
}

export class PoliceParams {
  filters: Array<FilterItem>;
  first: number;
  rows: number;
  sortField: string;
  globalFilter: string;
  multiSortMeta: boolean;
  sortOrder: number;

  constructor(params?) {

    if (params && Object.keys(params.filters).length > 0) {
      this.filters = [];
      for (const key of Object.keys(params.filters)) {
        if (key === 'global') {
          continue;
        }
        const filterItem = new FilterItem();
        filterItem.field = key.charAt(0).toUpperCase() + key.slice(1);
        filterItem.value = params.filters[key].value;
        this.filters.push(filterItem);
      }
    } else {
      this.filters = [];
    }

    if (params && params.first != null) {
      this.first = params.first;
    } else {
      this.first = 0;
    }

    if (params && params.rows != null) {
      this.rows = params.rows;
    } else {
      this.rows = 0;
    }

    if (params && params.sortField != null) {
      this.sortField = params.sortField;
    } else {
      this.sortField = '';
    }

    if (params && params.sortOrder != null) {
      this.sortOrder = params.sortOrder;
    } else {
      this.sortOrder = 0;
    }

    if (params && params.globalFilter != null) {
      this.globalFilter = params.globalFilter;
    } else {
      this.globalFilter = '';
    }

    if (params && params.multiSortMeta != null) {
      this.multiSortMeta = params.multiSortMeta;
    } else {
      this.multiSortMeta = false;
    }
  }
}
