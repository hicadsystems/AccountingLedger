<template>
    <div>
            <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-success"> {{ responseMessage }}</div>
        <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-label">Batch No</label>
              <select class="form-control" v-model="postBody.batchNo" name="batchNo" @change="gettoalamountperbatch">
                  <option v-for="batch in batchlist" v-bind:value="batch.batchNo" v-bind:key="batch.batchNo">{{batch.batchNo}}</option>
              </select>
            </div>
        </div>
          <div class="col-md-4">
            <div class="form-group">
               <h3>{{totalbatch}}</h3>
            </div>
        </div>
        </div>
        <div class="row">
         <div class="col-md-4">
            <div class="form-group">
                <label class="form-label">Bank</label>
              <select class="form-control" v-model="postBody.acctcode" name="acctcode" @change="gettotalbank" >
                  <option v-for="bank in banklist" v-bind:value="bank.acctcode" v-bind:key="bank.acctcode">{{bank.description}}</option>
              </select>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
              <h3> {{totalbank}}</h3>
            </div>
        </div>
        </div>
         <div class="col-12 ">
           <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="button" :disabled="!((totalbank ) >=  (totalbatch))">{{submitorUpdate}}</button></div>
        </div>
    </div>
</template>
<script>
export default {
  
    data(){
        return{
            responseMessage:'',
            errors:'',
            submitorUpdate : 'Submit',
            searchdata:'',
            batchlist:null,
            banklist:null,
            canProcess:true,
            totalbatch:null,
            totalbank:null,
        postBody:{
            BatchID:0,
            bankId:0,
            bankname:'',
            acctcode:'',
            batchNo:''
           

        }

        };
    },
    mounted(){
        this.$store.state.objectToUpdate=null
        axios
        .get('/Api/LoanRegister/getloanbyBatch')
        .then(response=>(this.batchlist=response.data))

       
       axios
            .get('/api/ChartofAccount/getChartofAccount4/4')
             .then(response => (this.banklist = response.data))

           
    },
    watch:{
        '$store.state.objectToUpdate': function(newval,oldval){

        }
    },
    methods:{
        gettoalamountperbatch(){
          axios
        alert(this.postBody.batchNo)
        .get(`/api/LoanRegister/getbatchtotal/${ this.postBody.batchNo}`)
         .then(response=>{this.totalbatch=response.data.data})
        },
          gettotalbank(){
          axios
        
        .get(`/api/LoanRegister/getbanktotal/${ this.postBody.acctcode}`)
         .then(response=>{this.totalbank=response.data.data})
    },
    checkForm: function (e) {
              
                    this.postPost();
               
            },
      postPost() {
       alert('Do you want to update Ledger?');
                axios.put(`/api/LoanRegister/ApproveLoanInBatch/${ this.postBody.batchNo}/${ this.postBody.acctcode}`)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription; this.canProcess = true;
                        if (response.data.responseCode == '200') {
                            this.submitorUpdate = 'Submit'
                            this.totalbatch = null; this.totalbank = null;
                            this.postBody.batchNo = ''; this.postBody.acctcode = '';

                        }
                    })
                    .catch(e => {
                        this.errors.push(e)
                    });

            }   
 }
 
    
};
</script>