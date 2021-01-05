<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Batch Number</label>
                            <select class="form-control" v-model="docno" name="docno"  @change="getListOfReciept" required>
                                <option v-for="rpt in finacialdocListRecieptno" v-bind:value="rpt.batchNo" v-bind:key="rpt.batchNo"> {{ rpt.batchNo }} </option>
                            </select>
                        </div>
                    </div>

            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Account Code</th>
                        <th>Account Name</th>
                        <th>Debit Amount</th>
                        <th>Credit Amount</th>
                        <th>Remarks</th>
                        



                    </tr>
                </thead>
                <tbody>
                    <tr v-for="findoc in finacialdocList">
                        <td>{{ findoc.acctcode }}</td>
                        <td>{{ findoc.accountname }}</td>
                        <td>{{ findoc.dbamt }}</td>
                        <td>{{ findoc.cramt }}</td>
                        <td>{{ findoc.remarks }}</td>
                        
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
                finacialdocList:null,
                finacialdocListRecieptno: null,
                docno:''
        };

            },
        

    mounted () {
        
            axios
            .get('/api/LoanRegister/getloanbyBatch2')
            .then(response => (this.finacialdocListRecieptno = response.data))
     },
       methods: {

           getListOfReciept(){
               axios
            .get(`/api/FinancialDoc/getAllDocument2/${ this.docno }`)
            .then(response => (this.finacialdocList = response.data))

            //console.log(this.docno)   
           }
       }

    
};
</script>