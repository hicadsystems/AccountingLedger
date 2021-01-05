<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Code</th>
                        <th>Description</th>
                        <th>Tenure</th>
                        <th>Fund Type</th>
                        <th>Interest</th>
                        <th>Loan Account</th>
                        <th>Income Account</th>
                        <th>Interest Account</th>




                    </tr>
                </thead>
                <tbody>
                    <tr v-for="LoanType in loantypeList">
                        <td>{{ LoanType.code }}</td>
                        <td>{{ LoanType.description }}</td>
                        <td>{{ LoanType.tenure }}</td>
                        <td>{{ LoanType.fundtypedesc }}</td>
                        <td>{{ LoanType.interest }}</td>
                        <td>{{ LoanType.loanacct }}</td>
                        <td>{{ LoanType.incomeacct }}</td>
                        <td>{{ LoanType.interestacct }}</td>

                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(LoanType)">Edit</button></td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processDelete(LoanType.id)">Delete</button></td>
                    </tr>
                </tbody>
              
            </table>
    </div>

    </div>

        <!-- END WRAPPER -->
</template>

<script>
export default { 
    
            data() {
                return {
                loantypeList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },
         watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
            this.getallloantype();
        }
    },
    mounted () {
        this.getallloantype()
     },
     methods: {
 
        processRetrieve : function (LoanType) {
            this.$store.state.objectToUpdate = LoanType;
         },
         getallloantype: function () {
             axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.loantypeList = response.data))
         },
          processDelete: function (id) {

             axios.get(`/api/LoanType/RemoveLoanType/${id}`)
                 .then(response => {
                     if (response.data.responseCode == '200') {
                         alert("loan type successfully deleted");
                         this.getallloantype();
                     }
                 }).catch(e => {
                            this.errors.push(e)
                        });

         }
    }
    
};
</script>