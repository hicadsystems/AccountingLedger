<template>
  <div class="p-3">
    <!-- Date Filters -->
    <div class="row mb-4">
      <div class="col">
        <label class="form-label">From Date:</label>
        <input type="date" v-model="fromDate" class="form-control" />
      </div>
      <div class="col">
        <label class="form-label">To Date:</label>
        <input type="date" v-model="toDate" class="form-control" />
      </div>
    </div>
                <div class="row" style="position:relative;top:-10px;">
                    <div class="col-4">
                        <div class="col-6">
                            <div class="btn-group mr-2 sw-btn-group-extra" role="group">
                              <a class="btn btn-submit btn-primary" :href="'/InvestRegisterMvc/InvestmentMoneyReport?startdate='+ this.fromDate +'&enddate='+ this.toDate" target="_blank" type="button">{{submitorUpdate2}}</a></div>
                        </div>
                    </div>
                </div>
                 
    <!-- Data Table -->
    <div class="card-body p-3">
      <table id="datatables-buttons" class="table table-striped table-bordered" style="width:100%">
        <thead>
          <tr>
            <th>Issuance Bank</th>
            <th>Voucher</th>
            <th>Description</th>
            <th>Amount</th>
            <th>Date</th>
            <th>DueDate</th>
            <th>InvestmentType</th>
            <th>Matured Amt</th>
            <th>Matured Date</th>
            <th>Interest</th>
            <th>Tenure</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="fundrate in filteredList" :key="fundrate.voucher">
            <td>{{ fundrate.issuancebank }}</td>
            <td>{{ fundrate.voucher }}</td>
            <td>{{ fundrate.description }}</td>
            <td>{{ fundrate.amount }}</td>
            <td>{{ fundrate.date }}</td>
            <td>{{ fundrate.dueDate }}</td>
            <td>{{ fundrate.investmentType }}</td>
            <td>{{ fundrate.maturedamt }}</td>
            <td>{{ fundrate.maturingdate }}</td>
            <td>{{ fundrate.interest }}</td>
            <td>{{ fundrate.tenure }}</td>
            <td>
              <button type="button" class="btn btn-sm btn-primary" @click="processRetrieve(fundrate)">
                Edit
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      InvestList: [],
      fromDate: null,
      toDate: null,
      submitorUpdate2:'Print',
    };
  },
  computed: {
    filteredList() {
      return this.InvestList.filter(item => {
        const itemDate = new Date(item.date);
        const from = this.fromDate ? new Date(this.fromDate) : null;
        const to = this.toDate ? new Date(this.toDate) : null;

        if (from && itemDate < from) return false;
        if (to && itemDate > to) return false;

        return true;
      });
    }
  },
  created() {
    this.$store.state.objectToUpdate = null;
  },
  watch: {
    '$store.state.objectToUpdate': function () {
      this.getAllInvestment();
    }
  },
  mounted() {
    this.getAllInvestment();
  },
  methods: {
    processRetrieve(invest) {
      this.$store.state.objectToUpdate = invest;
    },
    getAllInvestment() {
      axios.get('/api/PfInvest/getAllRegister')
        .then(response => {
          this.InvestList = response.data;
        });
    }
  }
};
</script>
