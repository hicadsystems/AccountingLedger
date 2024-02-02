<template>
    <!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/PfInvest/createInvestRegister" method="post">
            <div class="card-body">
                <input class="form-control" name="Id" v-model="postBody.ID" hidden />
                <div class="row">

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Date</label>
                            <vuejsDatepicker input-class="form-control" v-model="postBody.date" name="date"></vuejsDatepicker>
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Company</label>
                            <input class="form-control" name="company" v-model="postBody.company" placeholder="" />


                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Registrar</label>
                            <!-- <select class="form-control" v-model="postBody.IssuanceBankId" name="IssuanceBankId">
                                <option v-for="bk in bankList" v-bind:value="bk.id" v-bind:key="bk.id">{{ bk.bankname }}</option>
                            </select> -->
                            <select class="form-control" v-model="postBody.IssuanceBankId" name="IssuanceBankId" required>
                                    <option v-for="coa in chartofAccountList" v-bind:value="coa.id" v-bind:key="coa.id"> {{ coa.description }} </option>
                                </select>
                        </div>
                    </div>

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Stock</label>
                            <select class="form-control" v-model="postBody.StockId" name="StockId" required>
                                    <option v-for="coa in StockList" v-bind:value="coa.id" v-bind:key="coa.id"> {{ coa.description }} </option>
                                </select>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Transaction Type</label>
                            <select class="form-control" v-model="postBody.transactionType" name="transactionType" required>
                                    <option value="Buy">Buy</option>
                                    <option value="Sell">Sell</option>
                            </select>
                        </div>
                    </div>

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Voucher</label>
                            <input class="form-control" name="voucher" v-model="postBody.voucher" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Amount</label>
                            <input class="form-control" name="amount" v-model="postBody.amount" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Unit</label>
                            <input class="form-control" name="unit" v-model="postBody.unit" placeholder="" />

                        </div>
                     </div>
                </div>
                <div class="row">
                    <div class="col-12 col-x1-6">
                        <div class="form-group">
                            <label class="form-label">Remark</label>
                            <textarea class="form-control" name="description" v-model="postBody.description" placeholder=""/>
                        </div>
                    </div>
                    
                    </div>
                <div class="row">
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                    </div>
                </div>
            </div>
        </form>
    </div>

    <!-- END WRAPPER -->
</template>

<script>
    
     import vuejsDatepicker from 'vuejs-datepicker';
    export default {
        
        components: {
            vuejsDatepicker
        },
    data() {
        return {
            errors: null,
            responseMessage: '',
            submitorUpdate: 'Submit',
            canProcess: true,
            userList: null,
            bankList: null,
            StockList: null,
            InvestTypeList: null,
            chartofAccountList: null,
            readonly:true,
            postBody: {
                companyid: '',
                IssuanceBankId: '',
                interest: '',
                tenure:'',
                voucher: '',
                description: '',
                amount: 0,
                date: '',
                duedate: '',
                investmenttype:'',
                transactionType:'',
                chequeno: '',
                maturedamt: 0,
                unit:'',
                StockId:''
                
            },
            invest: [
                    { value: 'Money Market', text: 'Money Market' },
                    { value: 'Capital Market', text: 'Capital Market' }
                ]
        }
        },
      mounted () {
        axios
            .get('/api/Bank/getAllBanks')
              .then(response => (this.bankList = response.data)),
              axios
            .get('/api/Stock/getAllStocks')
              .then(response => (this.StockList = response.data)),
           axios
            .get('/api/PfInvest/LoadAllPersonnel')
                  .then(response => (this.userList = response.data))
          axios
                .get('/api/ChartofAccount/getAllChartofAccounts')
                .then(response => {

                    this.chartofAccountList = response.data
                    this.chartofAccountsStandBy = response.data

                })
     },

    watch:{
        '$store.state.objectToUpdate': function (FundCode, Rate, Period, Intrest) {

                this.postBody.company = this.$store.state.objectToUpdate.company,
                this.postBody.IssuanceBankId = this.$store.state.objectToUpdate.issuanceBankId,
                this.postBody.voucher = this.$store.state.objectToUpdate.voucher,
                this.postBody.description = this.$store.state.objectToUpdate.description,
                this.postBody.amount = this.$store.state.objectToUpdate.amount,
                this.postBody.date = this.$store.state.objectToUpdate.date,
                this.postBody.InvestmentType = this.$store.state.objectToUpdate.investmentType,
                this.postBody.chequeno = this.$store.state.objectToUpdate.chequeno,
                this.postBody.tenure = this.$store.state.objectToUpdate.tenure,
                this.postBody.interest = this.$store.state.objectToUpdate.interest,
                this.postBody.receivingbankid = this.$store.state.objectToUpdate.receivingbankid,
                this.postBody.unit = this.$store.state.objectToUpdate.unit,
                this.postBody.transactionType = this.$store.state.objectToUpdate.transactionType,
                this.postBody.StockId = this.$store.state.objectToUpdate.stockId
                this.postBody.Id = this.$store.state.objectToUpdate.id
                this.submitorUpdate = 'Update';

        }
    },
        methods: {
            onChange(event) {
                if (event.target.value == 'Money Market') {
                    this.readonly = false;
                } else {
                    this.readonly = true;
                }
            },
          checkForm: function (e) {
            e.preventDefault();

           
         if (this.postBody.company) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

            this.errors = [];
            this.errors.push('company is Required');
          }
        },
        postPost() {

                if(this.submitorUpdate == 'Submit'){
                    alert(this.postBody);
                    axios.post(`/api/PfInvest/createInvestCapitalMarket`, this.postBody )
                        .then(response => {
                          
                            this.responseMessage = response.data.responseDescription; this.canProcess = true;

                            if (response.data.responseCode == '200') {

                               // this.$store.state.objectToUpdate = "create";
                                this.postBody.company='';
                                this.postBody.issuanceBankId = '';
                                 this.postBody.receivingbankid = '';
                                this.postBody.voucher = '';
                                this.postBody.description= '';
                                this.postBody.amount= 0;
                                this.postBody.date='';
                                this.postBody.investmenttype= '';
                                this.postBody.closecode = '';
                                this.postBody.chequeno = '';
                                this.unit = '';
                                this.interest = '';
                                this.tenure = '';
                                this.postBody.transactionType = '';
                                this.postBody.stockId = '';
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            if (this.submitorUpdate == 'Update') {
                    axios.put(`/api/PfInvest/updateInvestCapitalMarket`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.submitorUpdate = 'Submit';
                                this.$store.state.objectToUpdate = "Update";
                                this.postBody.companyid='';
                                this.postBody.issuanceBankId = '';
                                this.postBody.voucher = '';
                                this.postBody.description= '';
                                this.postBody.amount= 0;
                                this.postBody.date='';
                                this.postBody.investmenttype= '';
                                this.postBody.closecode = '';
                                this.postBody.chequeno = '';
                                this.postBody.transactionType = '';
                                this.postBody.stockId = '';
                                this.unit = '';
                                this.interest = '';
                                this.tenure = '';
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter(){
                let objecttoedit = this.$store.state.objectToUpdate;

                if (objecttoedit.Id) {
                    this.postBody.company = objecttoedit.company;
                    this.postBody.issuanceBankId = objecttoedit.issuanceBankId;
                    this.postBody.voucher = objecttoedit.voucher;
                    this.postBody.description = objecttoedit.description;
                    this.postBody.amount = objecttoedit.amount;
                    this.postBody.date = objecttoedit.date;
                    this.postBody.investmenttype = objecttoedit.investmenttype;
                    this.postBody.closecode = objecttoedit.closecode;
                    this.postBody.chequeno = objecttoedit.chequeno;
                    this.postBody.unit = objecttoedit.unit;
                    this.postBody.tenure = objecttoedit.tenure;
                    this.postBody.interest = objecttoedit.interest;
                    this.postBody.transactionType = objecttoedit.transactionType;
                    this.postBody.stockId = objecttoedit.stockId;
                }
            }
        }
};
</script>