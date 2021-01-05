<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
     
                        <th>Loan Type</th>
                        <th>Interest</th>
                        <th>Review Date</th>




                    </tr>
                </thead>
                <tbody>
                    <tr v-for="LoanType in loantypeList">
                        <td>{{ LoanType.loantypedesc }}</td>
                        <td>{{ LoanType.interestrate }}</td>
                        <td>{{ LoanType.reviewDate }}</td>


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
            .get('/api/LoanType/getAllLoanDescr')
            .then(response => (this.loantypeList = response.data))
         },
          processDelete: function (id) {
alert(id);
             axios.get(`/api/LoanType/RemoveLoanTyper/${id}`)
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