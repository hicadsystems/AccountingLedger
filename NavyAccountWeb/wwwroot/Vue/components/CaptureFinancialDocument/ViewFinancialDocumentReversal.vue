<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Reciept Number</label>
                            <select class="form-control" v-model="docno" name="docno"  @change="getListOfReciept" required>
                                <option v-for="rpt in finacialdocListRecieptno" v-bind:value="rpt.docno" v-bind:key="rpt.docno"> {{ rpt.docno }} </option>
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
             <div class="col-6 col-xl-4">
                    <div class="btn-group mr-2 sw-btn-group-extra" role="group">
                        <button class="btn btn-submit btn-primary" type="button" @click="submitReceipt" > Reverse </button>
                    </div>
                </div>
    </div>

    </div>

        <!-- END WRAPPER -->
</template>

<script>
export default { 
    
            data() {
                return {
                finacialdocList: null,
                finacialdocListKeep:null,
                finacialdocListRecieptno: null,
                docno:''
        };

            },
        

    mounted () {
        
            this.documentListLoader()
     },
       methods: {
           documentListLoader() {
               axios
            .get('/api/FinancialDoc/getAllRecieptNo')
                .then(response => {

                this.finacialdocListRecieptno = response.data
                this.finacialdocListKeep = response.data
                   let finacialdocListWithR = this.finacialdocListRecieptno.filter(function (el) {
                        return el.docno.endsWith('R')
                   })
                     let getTheValuesWithout = finacialdocListWithR.map(o => Object.assign({}, o, { docno: o.docno.substring(0,o.docno.length -1) }))
                    let valuestoKnockOffCombined = getTheValuesWithout.concat(finacialdocListWithR)
                    valuestoKnockOffCombined = valuestoKnockOffCombined.map(o => o.docno) 
                    this.finacialdocListRecieptno = this.finacialdocListRecieptno.filter(function (el) {
                        return !valuestoKnockOffCombined.includes(el.docno)
                    })
                })
           },
           getListOfReciept(){
               axios
            .get(`/api/FinancialDoc/getAllDocument/${ this.docno }`)
                   .then(response => (this.finacialdocList = response.data))

           },
           submitReceipt() {

               if (confirm("Are you sure you want to Reverse?")) {
                   axios
                       .get(`/api/FinancialDoc/getAllDocumentReversal/${this.docno}`)
                       .then(response => {
                       this.finacialdocList = response.data
                           if (response.data.responseCode === "200") {
                               this.documentListLoader()
                           }
                       }) 
               }
           }
       }

    
};
</script>