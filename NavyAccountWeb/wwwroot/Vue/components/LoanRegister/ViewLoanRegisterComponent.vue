<template>
    	<!-- WRAPPER -->
    <div>
      
        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Service No</th>
                        <th>Full Name</th>
                        <th>Loan Type</th>
                        <th>Tenure(Months)</th>
                        <th>Amount</th>
                        <th>Remarks</th>
                        <th>Status</th>
                        <th>Status and Date</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="loanReg in loanregisterList">
                        <td>{{ loanReg.svcno }}</td>
                        <td>{{ loanReg.firstName }} {{ loanReg.lastName }}   {{ loanReg.middleName }}</td>
                        <td>{{ loanReg.loanTypeDesc }}</td>
                        <td>{{ loanReg.tenure }}</td>
                        <td>{{ loanReg.amount }}</td>
                        <td>{{ loanReg.remarks }}</td>
                        <td>{{ loanReg.statusAndDate }}</td>
                        <td>{{ retrieveStatus(loanReg.statusId) }}</td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(loanReg)">Update</button></td>
                    </tr>
                </tbody>

            </table>
        </div>

    </div>

        <!-- END WRAPPER -->
</template>

<script>
export default { 
    props:['fundcodeid','statusdef','isapplication'],
            data() {
                return {
                    loanregisterList: null,
                    loanStatusList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },
        beforeMount() {
             axios
            .get('/api/LoanStatus/getAllLoanStatus')
                .then(response => (this.loanStatusList = response.data))
        },
          watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
            this.getallloan();
            this.getallloan2();
            this.getallloan3();
        }
    },
        mounted() {
            this.getallloan();
            this.getallloan2();
            this.getallloan3();

        },
        methods: {
            getallloan() {

                if (this.isapplication == 'no') {
                    axios
                        .get(`/api/LoanRegister/getAllLoanRegisterByStatus/${this.fundcodeid}/${this.statusdef}`)
                        .then(response => (this.loanregisterList = response.data))
                }
            },
             getallloan2() {
            if (this.isapplication == 'yes') {
               
                    axios
                        .get(`/api/LoanRegister/getAllLoanRegisterByRangeStatus/${this.fundcodeid}/${this.statusdef}`)
                        .then(response => (this.loanregisterList = response.data))
                }
            },
            getallloan3() {
                if (this.isapplication == 'progress') {
                   
                    axios
                        .get(`/api/LoanRegister/getAllLoanRegisterByRangeStatusP/${this.fundcodeid}/${this.statusdef}`)
                        .then(response => (this.loanregisterList = response.data))
                }
            },
 
        processRetrieve : function (loanReg) {
            this.$store.state.objectToUpdate = loanReg;
         },
         retrieveStatus: function (statusid) {
             return this.loanStatusList.filter(x => x.id == statusid)[0].description;
         }

    }
    
};
</script>