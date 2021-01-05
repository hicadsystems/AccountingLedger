<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">

            <div class="row">
                <div class="col-md-4">
                    <select class="form-control" v-model="postBody.trialbalance">
                        <option v-for="b in balance" v-bind:value="b.value" v-bind:key="b.value">{{ b.text }}</option>
                    </select>
                </div>
                <div class="col-md-1">
                    <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" v-on:click="exportPDF()" type="submit">Search and print</button></div>
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
                    trialbalanceList: null,
                    postBody: {
                        trialbalance:''
                    },
                    balance: [
                        { value: 'All', text: 'All' },
                        { value:'Specific Main ledger',text:'Specific Main ledger'}
                    ]
                }
            },
            methods: {
         exportPDF() {
             axios
                 .get(`/api/TrialBalance/GenerateReport/${ this.postBody.trialbalance }`)
                 .then(response => {
                     if (response.data.responseCode == '200') {
                         this.trialbalanceList = response.data;

                         var vm = this
                         var columns = [
                             { code: "Code", datakey: "code" },
                             { description: "description", datakey: "description" },
                             { debit: "debit", datakey: "debit" },
                             { credit:"credit", datakey:"credit"}
                         ];

                         var doc = new jsPDF('p', 'pt');
                         doc.text('Trial balance Report');
                         doc.autoTable(columns, this.trialbalanceList, {
                             margin: { top: 60 },
                         });
                         doc.save('balance.pdf');

                     }
                 })

                 
         }
        
    }

};
</script>